namespace MainServer.Controllers;

public class ProcessingNotFinishedException(string message) : Exception(message);