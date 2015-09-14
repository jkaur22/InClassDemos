<Query Kind="Program" />

void Main()
{
	//"hello " +"don" +" world" 
	//5+7
	//string name = "jaspreet";
	//string message = "hello " + name +" world";
	//message.Dump();
	SayHello("jaspreet");
}

// Define other methods and classes here
public void SayHello(string name)
{
string message = "hello " + name +" world";
	message.Dump();
}