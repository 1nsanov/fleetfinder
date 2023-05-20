using Firebase.Storage;
using fleetfinder.service.main.application.Common.Enums;
using fleetfinder.service.main.application.Common.Exceptions;
using fleetfinder.service.main.application.Common.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace fleetfinder.service.main.application.Services;

public class ImageService : IImageService
{
    private readonly CommandDbContext _commandDbContext;
    private readonly IConfiguration _config;

    public ImageService(CommandDbContext commandDbContext, IConfiguration config)
    {
        _commandDbContext = commandDbContext;
        _config = config;
    }

    public async void DeleteImage(FirebaseStorageFolder folder, long id, string imageUrl, CancellationToken cancellationToken)
    {
        try
        {
            var firebaseStorage = new FirebaseStorage(_config["FirebaseStorage:Bucket"]);
           
            var fileName = imageUrl
                .Split("/").Last()
                .Split("?").First()
                .Split("%").Last()[2..];
            
            await firebaseStorage
                .Child(_config[$"FirebaseStorage:Folders:{folder.ToString()}"])
                .Child(fileName)
                .DeleteAsync();
            
            SaveImage(folder, id, imageUrl, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new Exception( $"An error occurred: {ex.Message}");
        }
    }
    
    public async void SaveImage(FirebaseStorageFolder folder, long id, string url, CancellationToken cancellationToken, bool isDelete = false)
    {
        switch (folder)
        {
            case FirebaseStorageFolder.CargoTransport:
                var ct = await _commandDbContext.CargoTransport.FindAsync(new object?[] { id }, cancellationToken: cancellationToken)
                                     ?? throw new EntityNotFoundException(id);
                if (!isDelete)
                    ct.Images.Add(new() { TransportId = id, Url = url });
                else
                    ct.Images.Remove(
                        await _commandDbContext.CargoTransportImage.FirstOrDefaultAsync(x => x.Url == url, cancellationToken: cancellationToken) 
                        ?? throw new EntityNotFoundException(url));
                break;
            case FirebaseStorageFolder.PassengerTransport:
                var pt = await _commandDbContext.PassengerTransport.FindAsync(new object?[] { id }, cancellationToken: cancellationToken)
                                     ?? throw new EntityNotFoundException(id);
                if (!isDelete)
                    pt.Images.Add(new() { TransportId = id, Url = url });
                else
                    pt.Images.Remove(
                        await _commandDbContext.PassengerTransportImage.FirstOrDefaultAsync(x => x.Url == url, cancellationToken: cancellationToken) 
                        ?? throw new EntityNotFoundException(url));
                break;
            case FirebaseStorageFolder.SpecialTransport:
                var st = await _commandDbContext.SpecialTransport.FindAsync(new object?[] { id }, cancellationToken: cancellationToken)
                         ?? throw new EntityNotFoundException(id);
                if (!isDelete)
                    st.Images.Add(new() { TransportId = id, Url = url });
                else
                    st.Images.Remove(
                        await _commandDbContext.SpecialTransportImage.FirstOrDefaultAsync(x => x.Url == url, cancellationToken: cancellationToken) 
                        ?? throw new EntityNotFoundException(url));
                break;
            case FirebaseStorageFolder.CargoOrder:
                var co = await _commandDbContext.CargoOrder.FindAsync(new object?[] { id }, cancellationToken: cancellationToken)
                         ?? throw new EntityNotFoundException(id);
                if (!isDelete)
                    co.Images.Add(new() { OrderId = id, Url = url });
                else
                    co.Images.Remove(
                        await _commandDbContext.CargoOrderImage.FirstOrDefaultAsync(x => x.Url == url, cancellationToken: cancellationToken) 
                        ?? throw new EntityNotFoundException(url));
                break;
            case FirebaseStorageFolder.PassengerOrder:
                var po = await _commandDbContext.PassengerOrder.FindAsync(new object?[] { id }, cancellationToken: cancellationToken)
                         ?? throw new EntityNotFoundException(id);
                if (!isDelete)
                    po.Images.Add(new() { OrderId = id, Url = url });
                else
                    po.Images.Remove(
                        await _commandDbContext.PassengerOrderImage.FirstOrDefaultAsync(x => x.Url == url, cancellationToken: cancellationToken) 
                        ?? throw new EntityNotFoundException(url));
                break;
            case FirebaseStorageFolder.SpecialOrder:
                var so = await _commandDbContext.SpecialOrder.FindAsync(new object?[] { id }, cancellationToken: cancellationToken)
                         ?? throw new EntityNotFoundException(id);
                if (!isDelete)
                    so.Images.Add(new() { OrderId = id, Url = url });
                else
                    so.Images.Remove(
                        await _commandDbContext.SpecialOrderImage.FirstOrDefaultAsync(x => x.Url == url, cancellationToken: cancellationToken) 
                        ?? throw new EntityNotFoundException(url));
                break;
            case FirebaseStorageFolder.UserProfile:
                var up = await _commandDbContext.User.FindAsync(new object?[] { id }, cancellationToken: cancellationToken)
                         ?? throw new EntityNotFoundException(id);
                up.ImageUrl = !isDelete ? url : null;
                break;
        }
        
        await _commandDbContext.SaveChangesAsync(cancellationToken);
    }
}