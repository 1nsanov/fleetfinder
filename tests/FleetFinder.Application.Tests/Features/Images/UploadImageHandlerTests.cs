using FleetFinder.Application.Abstractions.Storage;
using FleetFinder.Application.Common.Enums;
using FleetFinder.Application.Features.Images;
using FleetFinder.Application.Features.Images.Upload;

namespace FleetFinder.Application.Tests.Features.Images;

public class UploadImageHandlerTests
{
    private static UploadImage.Handler CreateHandler(IObjectStorageService? storage = null)
    {
        storage ??= Substitute.For<IObjectStorageService>();
        return new UploadImage.Handler(storage);
    }

    private static UploadImage.FileUpload File(
        string name,
        string contentType,
        long length) =>
        new(name, contentType, length, Stream.Null);

    [Fact]
    public async Task Handle_Uploads_WhenJpegAndPngAreValid()
    {
        var storage = Substitute.For<IObjectStorageService>();
        storage.UploadAsync(
                Arg.Any<StorageFolder>(),
                Arg.Any<string>(),
                Arg.Any<Stream>(),
                Arg.Any<string>(),
                Arg.Any<CancellationToken>())
            .Returns(call => $"https://cdn.example/{call.ArgAt<string>(1)}");
        var handler = CreateHandler(storage);
        var request = new UploadImage.RequestDto(
            StorageFolder.UserProfile,
            [File("a.jpg", "image/jpeg", 1024), File("b.png", "image/png", 2048)]);

        var urls = await handler.Handle(new UploadImage.Command(request), CancellationToken.None);

        urls.Should().HaveCount(2);
        await storage.Received(2).UploadAsync(
            StorageFolder.UserProfile,
            Arg.Any<string>(),
            Arg.Any<Stream>(),
            Arg.Any<string>(),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_Throws_WhenContentTypeIsNotAllowed()
    {
        var handler = CreateHandler();
        var request = new UploadImage.RequestDto(
            StorageFolder.UserProfile,
            [File("a.gif", "image/gif", 1024)]);

        var act = () => handler.Handle(new UploadImage.Command(request), CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*JPEG and PNG*");
    }

    [Fact]
    public async Task Handle_Throws_WhenExtensionDoesNotMatchMime()
    {
        var handler = CreateHandler();
        var request = new UploadImage.RequestDto(
            StorageFolder.UserProfile,
            [File("a.svg", "image/jpeg", 1024)]);

        var act = () => handler.Handle(new UploadImage.Command(request), CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*JPEG and PNG*");
    }

    [Fact]
    public async Task Handle_Throws_WhenFileExceedsMaxSize()
    {
        var handler = CreateHandler();
        var request = new UploadImage.RequestDto(
            StorageFolder.UserProfile,
            [File("a.jpg", "image/jpeg", ImageUploadLimits.MaxFileBytes + 1)]);

        var act = () => handler.Handle(new UploadImage.Command(request), CancellationToken.None);

        await act.Should().ThrowAsync<ArgumentException>().WithMessage("*10 MB*");
    }
}
