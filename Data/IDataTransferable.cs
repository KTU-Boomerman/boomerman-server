namespace BoomermanServer.Data
{
    public interface IDataTransferable<DTO>
    {
        DTO ToDTO();
    }
}
