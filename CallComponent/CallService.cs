
using Core;

namespace CallComponent;

public class CallService(ICallRepository repository) : ICallService
{
    public async Task<CallId> ProcessCallAsync(Audio audio)
    {

    }

}

public interface ICallRepository
{
    CallId GetCallId(Uri uri);

}