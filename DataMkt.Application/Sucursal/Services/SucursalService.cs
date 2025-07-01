using DataMkt.Application.Sucursal.Dto;
using DataMkt.Application.Sucursal.Mapping;
using DataMkt.Domain.Repositories;

namespace DataMkt.Application.Sucursal.Services;

public class SucursalService : ISucursalService
{
    private readonly ISucursalRepository _repo;
    public SucursalService(ISucursalRepository repo) => _repo = repo;

    public async Task<IEnumerable<SucursalDto>> GetSucursalesAsync(CancellationToken ct = default) =>
        (await _repo.GetAllAsync(ct)).Select(s => s.ToDto());

    public async Task<SucursalDto> CreateSucursalAsync(SucursalDto dto, CancellationToken ct = default)
    {
        var added = await _repo.AddAsync(dto.ToEntity(), ct);
        return added.ToDto();
    }

    public async Task DeleteSucursalAsync(int id, CancellationToken ct = default)
    {
        var entity = await _repo.GetByIdAsync(id, ct) ?? throw new KeyNotFoundException("Sucursal no encontrada");
        await _repo.RemoveAsync(entity, ct);
    }
}
