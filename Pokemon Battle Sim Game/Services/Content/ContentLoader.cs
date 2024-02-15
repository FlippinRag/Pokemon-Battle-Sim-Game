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
        private readonly ContentManager contentManager;
        private readonly Dictionary<string, Texture2D> texture;
        private readonly Dictionary<string, SpriteFont> font;

        public ContentLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            texture = new Dictionary<string, Texture2D>();
            font = new Dictionary<string, SpriteFont>();
        }

        public Texture2D LoadTexture(string textureName)
        {
            if (texture.TryGetValue(textureName, out var loadTexture)) return loadTexture;
            try
            {
                var texture = contentManager.Load<Texture2D>(Path.Combine("Textures", textureName));
                this.texture.Add(textureName, texture);
                return texture;
            }
            catch (Exception) when (textureName != TextureNotFound)
            {
                return LoadTexture(TextureNotFound);
            }
        }

        public SpriteFont LoadFont(string fontName)
        {
            if (string.IsNullOrWhiteSpace(fontName))
            {
                throw new ArgumentException("Font name cannot be null, empty or consist only of white-space characters", nameof(fontName));
            }

            if (font.TryGetValue(fontName, out var loadFont)) return loadFont;
            try
            {
                var fontFilePath = Path.Combine("Fonts", fontName);
                Console.WriteLine($"Loading font: {fontName}");
                Console.WriteLine($"Font file path: {fontFilePath}");

                var font = contentManager.Load<SpriteFont>(fontFilePath);
                this.font.Add(fontName, font);
                return font;
            }
            catch (Exception) when (fontName != FontNotFound)
            {
                return LoadFont(FontNotFound);
            }
        }
    }
}
