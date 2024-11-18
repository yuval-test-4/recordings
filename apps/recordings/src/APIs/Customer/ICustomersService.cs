using Recordings.APIs.Common;
using Recordings.APIs.Dtos;

namespace Recordings.APIs;

public interface ICustomersService
{
    /// <summary>
    /// Create one customer
    /// </summary>
    public Task<Customer> CreateCustomer(CustomerCreateInput customer);

    /// <summary>
    /// Delete one customer
    /// </summary>
    public Task DeleteCustomer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many customers
    /// </summary>
    public Task<List<Customer>> Customers(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about customer records
    /// </summary>
    public Task<MetadataDto> CustomersMeta(CustomerFindManyArgs findManyArgs);

    /// <summary>
    /// Get one customer
    /// </summary>
    public Task<Customer> Customer(CustomerWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one customer
    /// </summary>
    public Task UpdateCustomer(CustomerWhereUniqueInput uniqueId, CustomerUpdateInput updateDto);

    /// <summary>
    /// Connect multiple videos records to customer
    /// </summary>
    public Task ConnectVideos(CustomerWhereUniqueInput uniqueId, VideoWhereUniqueInput[] videosId);

    /// <summary>
    /// Disconnect multiple videos records from customer
    /// </summary>
    public Task DisconnectVideos(
        CustomerWhereUniqueInput uniqueId,
        VideoWhereUniqueInput[] videosId
    );

    /// <summary>
    /// Find multiple videos records for customer
    /// </summary>
    public Task<List<Video>> FindVideos(
        CustomerWhereUniqueInput uniqueId,
        VideoFindManyArgs VideoFindManyArgs
    );

    /// <summary>
    /// Update multiple videos records for customer
    /// </summary>
    public Task UpdateVideos(CustomerWhereUniqueInput uniqueId, VideoWhereUniqueInput[] videosId);
}
