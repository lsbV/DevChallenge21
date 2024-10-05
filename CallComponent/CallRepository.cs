using AutoMapper;
using Core;
using Core.Exceptions;
using Database;
using Microsoft.EntityFrameworkCore;

namespace CallComponent;

public class CallRepository(AppDbContext context, IMapper mapper) : ICallRepository
{
    public async Task<Call> GetCallByIdAsync(CallId id)
    {
        var call = await context.Calls
            .Include(c => c.Topics)
            .FirstOrDefaultAsync(c => c.Id == id.Value);
        if (call == null) throw new EntityNotExistException(nameof(DbCall), id);
        context.Entry(call).State = EntityState.Detached;
        return mapper.Map<Call>(call);

    }
    public async Task<CallId> SaveCallAsync(Call call)
    {
        var dbCall = mapper.Map<DbCall>(call);
        await context.Calls.AddAsync(dbCall);
        await context.SaveChangesAsync();
        context.Entry(dbCall).State = EntityState.Detached;
        return new CallId(dbCall.Id);

    }
    public async Task<CallId> UpdateCallAsync(Call call)
    {
        var dbCall = mapper.Map<DbCall>(call);
        context.Calls.Update(dbCall);
        await context.SaveChangesAsync();
        context.Entry(dbCall).State = EntityState.Detached;
        return call.Id;
    }
}