using System;
using SignalRPlusAzureQueue.Interfaces;

public class FakeReader: IQueueReader
{
	public FakeReader()
	{
	}

    public void ConnectToQueue()
    {
        throw new NotImplementedException();
    }

    public int QueueCount()
    {
        throw new NotImplementedException();
    }

    public string GetMessage()
    {
        throw new NotImplementedException();
    }
}
