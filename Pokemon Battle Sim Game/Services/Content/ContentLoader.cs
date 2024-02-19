using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Pokemon_Battle_Sim_Game.Services.Content
{
    public class ContentLoader : IContentLoader
    {
        private const string TextureNotFound = "NotFoundTexture";
        private const string FontNotFound = "NotFoundFont";
        private readonly ContentManager mainContentManager;
        private readonly Dictionary<string, Texture2D> textures;
        private readonly Dictionary<string, SpriteFont> fonts;
        private readonly Dictionary<string, ContentManager> contentManagers;

        public ContentLoader(ContentManager mainContentManager)
        {
            this.mainContentManager = mainContentManager;
            textures = new Dictionary<string, Texture2D>();
            fonts = new Dictionary<string, SpriteFont>();
            contentManagers = new Dictionary<string, ContentManager>();
        }

        public Texture2D LoadTexture(string textureName)
        {
            if (textures.TryGetValue(textureName, out var loadTexture)) return loadTexture;
            try
            {
                Console.WriteLine($"Loading texture: {textureName}");
                var contentManager = new ContentManager(mainContentManager.ServiceProvider, mainContentManager.RootDirectory);
                Texture2D texture = contentManager.Load<Texture2D>(Path.Combine("Textures", textureName));
                textures.Add(textureName, texture);
                contentManagers.Add(textureName, contentManager);
                return texture;
            }
            catch (Exception) when (textureName != TextureNotFound)
            {
                return LoadTexture(TextureNotFound);
            }
        }

        public void UnloadTexture(string textureName)
        {
            if (contentManagers.TryGetValue(textureName, out var contentManager))
            {
                contentManager.Unload();
                contentManagers.Remove(textureName);
                textures.Remove(textureName);
            }
        }

        public SpriteFont LoadFont(string fontName)
        {
            if (string.IsNullOrWhiteSpace(fontName))
            {
                throw new ArgumentException("Font name cannot be null, empty or consist only of white-space characters", nameof(fontName));
            }

            if (fonts.TryGetValue(fontName, out var loadFont)) return loadFont;
            try
            {
                var fontFilePath = Path.Combine("Fonts", fontName);
                Console.WriteLine($"Loading font: {fontName}");
                Console.WriteLine($"Font file path: {fontFilePath}");

                var contentManager = new ContentManager(mainContentManager.ServiceProvider, mainContentManager.RootDirectory);
                var font = contentManager.Load<SpriteFont>(fontFilePath);
                fonts.Add(fontName, font);
                contentManagers.Add(fontName, contentManager);
                return font;
            }
            catch (Exception) when (fontName != FontNotFound)
            {
                return LoadFont(FontNotFound);
            }
        }
    }
}
