#include <restbed>
#include <iostream>
#include <fstream>
#include <cstdlib>
#include <cstring>
#include <memory>

using namespace std;
using namespace restbed;

// ########## VARIABLES ##########

std::string FilePath;

// ###############################

const std::string ReadFile(const std::string &path)
{
	ifstream file(path);
	
	if (file.is_open()) {
		std::string out;
		std::string buf;
		
		while(getline(file, buf))
			out += (buf + "\n");

		return out;
	}
	else {
		return "No output file present.";
	}
}

void get_method_handler( const shared_ptr< Session > session )
{
	cout << "Received GET /" << endl;
	
	// Read file contents and return them as response
	const std::string data = ReadFile(FilePath);
	
	// Close the session with file data
    session->close( OK, data, { { "Content-Length", to_string(data.length()) }, { "Connection", "close" } } );
}

int main(int argc, char **argv)
{
	cout << "Starting server..." << endl;
	
	const char* env = std::getenv("CPP_ENV");
	const char* port = std::getenv("SERVER_PORT");
	
	if (env != nullptr && strcmp(env, "PRODUCTION") == 0) {
		FilePath = "/data/output";
		cout << "PROD MODE" << endl;
	}
	else {
		FilePath = "/ex3/httpserv/output.txt";
		cout << "TEST MODE" << endl;
	}
	
	char lport[4];

	if (argc > 1) {
		strcpy(lport, argv[1]);
	}
	
	else if (port != nullptr) {
		strcpy(lport, port);
	}

	else {
		cout << "Port was not defined. Exiting." << endl;
		return EXIT_FAILURE;
	}

	std::cout << "Listening on 127.0.0.1::" << lport << std::endl;

    auto resource = make_shared< Resource >( );
    resource->set_path( "/" );
    resource->set_method_handler( "GET", get_method_handler );
    
    auto settings = make_shared< Settings >( );
	settings->set_port( atoi(lport) );
    
    Service service;
    service.publish( resource );
    service.start( settings );
    
    return EXIT_SUCCESS;
}
