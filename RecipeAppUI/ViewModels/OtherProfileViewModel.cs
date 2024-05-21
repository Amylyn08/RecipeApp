using Avalonia.Media.Imaging;
using RecipeApp.Context;
using RecipeApp.Models;
using RecipeApp.Searcher;
using RecipeApp.Services;
using ReactiveUI;
using System.Collections;
using System.Collections.Generic;
using RecipeAppUI.Utils;

namespace RecipeAppUI.ViewModels;

/// <summary>
/// ViewModel for viewing someone's elses profile
/// </summary>
public class OtherProfileViewModel : ViewModelBase {
    private string _username = null!;
    private string _description = null!;
    private byte[]? _profilePicture;
    private Bitmap? _bitmap;
    private List<Recipe> _othersPersonFav = null!;

    public string Username {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    public string Description {
        get => _description;
        set => this.RaiseAndSetIfChanged(ref _description, value);
    }

    public byte[]? ProfilePicture {
        get => _profilePicture;
        set => this.RaiseAndSetIfChanged(ref _profilePicture, value);
    }

    public Bitmap? Bitmap {
        get => _bitmap;
        set => this.RaiseAndSetIfChanged(ref _bitmap, value);
    }

    public List<Recipe> OtherPersonFavourites {
        get => _othersPersonFav;
        set => this.RaiseAndSetIfChanged(ref _othersPersonFav, value);
    }


    public OtherProfileViewModel(SplankContext context, User user) {
        OtherPersonFavourites = new SearchByUserFavorite(context, user).FilterRecipes();
        Username = user.Name;
        Description = user.Description;
        ProfilePicture = user.ProfilePicture;
        if (ProfilePicture != null) {
            Bitmap = BitMapper.DoBitmap(ProfilePicture);
        }
    }
}