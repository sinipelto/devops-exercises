BDIR = ./build
TARGET = server
OBJS = server.o

INC = -I./include
#LDFLAGS = -L./lib
LIBS = -pthread -lrestbed

CXX = g++
CXXFLAGS = -c -Wall -std=c++17 -O3

$(TARGET): $(OBJS)
	mkdir -p $(BDIR)
	$(CXX) -o $(BDIR)/$(TARGET) $(OBJS) $(LIBS)
#	$(CXX) -o $(BDIR)/$(TARGET) $(OBJS) $(LDFLAGS) $(LIBS)

$(TARGET).o: $(TARGET).cpp
	$(CXX) $(CXXFLAGS) $(INC) $<

clean:
	rm -rf $(BDIR) $(OBJS)
