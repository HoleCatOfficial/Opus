using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Opus.Content.Helpers;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace Opus.Content.OpusBook
{
    public static class OpusBookRegistry
    {
        private static readonly Dictionary<string, Dictionary<int, string>> books 
            = new Dictionary<string, Dictionary<int, string>>();

        public static void RegisterBook(string key, Dictionary<int, string> pages)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Book key cannot be null or empty.", nameof(key));

            books[key] = pages;
        }

        public static Dictionary<int, string> GetBook(string key)
        {
            if (books.TryGetValue(key, out var pages))
                return pages;

            return null;
        }

        public static bool Exists(string key) => books.ContainsKey(key);
    }


    public class OpusBookUI : UIState
    {
        private UIPanel panel;
        private UIText pageText;
        private UIText pageNumberText;
        private UIImageButton nextButton;
        private UIImageButton prevButton;
        private UIImageButton CloseButton;
        private bool dragging = false;
        private Vector2 offset;

        private List<string> pages = new List<string>();
        private int currentPage = 0;

        public static bool Visible = false;

        public override void OnInitialize()
        {
            if (Main.dedServ)
                return;

            panel = new UIPanel();
            panel.Width.Set(600f, 0f);
            panel.Height.Set(300f, 0f);
            panel.Left.Set(200f, 0f);
            panel.Top.Set(100f, 0f);
            panel.BackgroundColor = new Color(20, 20, 40, 200);

            panel.OnLeftMouseDown += StartDrag;
            panel.OnLeftMouseUp += EndDrag;
            panel.OnMouseOver += EnableMouseInterface;
            Append(panel);

            pageText = new UIText("", 1.0f, false);
            //pageText.Width.Set(-40f, 1f);
            //pageText.Height.Set(-60f, 1f);
            pageText.Width.Set(0f, 1f); // Full width of parent
            pageText.Height.Set(0f, 1f); // Full height of parent
            pageText.Left.Set(20f, 0f);
            pageText.Top.Set(20f, 0f);
            pageText.TextColor = Color.White;
            panel.Append(pageText);

            pageNumberText = new UIText("Page 1", 0.7f);
            pageNumberText.Top.Set(270f, 0f);
            pageNumberText.Left.Set(260f, 0f);
            panel.Append(pageNumberText);

            nextButton = new UIImageButton(ModContent.Request<Texture2D>($"Opus/Assets/Textures/ArrowRight"));
            nextButton.Width.Set(24f, 0f);
            nextButton.Height.Set(24f, 0f);
            nextButton.Left.Set(560f, 0f);
            nextButton.Top.Set(270f, 0f);
            nextButton.OnLeftClick += NextPage;
            panel.Append(nextButton);

            prevButton = new UIImageButton(ModContent.Request<Texture2D>($"Opus/Assets/Textures/ArrowLeft"));
            prevButton.Width.Set(24f, 0f);
            prevButton.Height.Set(24f, 0f);
            prevButton.Left.Set(10f, 0f);
            prevButton.Top.Set(270f, 0f);
            prevButton.OnLeftClick += PrevPage;
            panel.Append(prevButton);

            CloseButton = new UIImageButton(ModContent.Request<Texture2D>($"Opus/Assets/Textures/CloseButton"));
            CloseButton.Width.Set(24f, 0f);
            CloseButton.Height.Set(24f, 0f);
            CloseButton.Left.Set(8f, 0f);
            CloseButton.Top.Set(8f, 0f);
            CloseButton.OnLeftClick += Close;
            panel.Append(CloseButton);
        }

        public override void OnActivate()
        {


        }

        public void OpenBook(string bookKey)
        {
            if (!OpusBookRegistry.Exists(bookKey))
            {
                pages.Clear();
                pages.Add("This book has not been registered.");
                currentPage = 0;
                UpdatePageText();
                Visible = true;
                return;
            }

            var bookPages = OpusBookRegistry.GetBook(bookKey);
            pages.Clear();

            foreach (var kv in bookPages.OrderBy(kv => kv.Key))
            {
                pages.Add(kv.Value);
            }

            currentPage = 0;
            UpdatePageText();
            Visible = true;
        }

        private void StartDrag(UIMouseEvent evt, UIElement listeningElement)
        {
            dragging = true;
            offset = new Vector2(evt.MousePosition.X - panel.Left.Pixels, evt.MousePosition.Y - panel.Top.Pixels);
            // evt.StopPropagation(); // Prevents weird event bubbling
        }

        private void EndDrag(UIMouseEvent evt, UIElement listeningElement)
        {
            dragging = false;
        }

        private void EnableMouseInterface(UIMouseEvent evt, UIElement listeningElement)
        {
            Main.LocalPlayer.mouseInterface = true;
        }


        private void NextPage(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(new SoundStyle("Opus/Assets/Audio/Pageturn"), Main.LocalPlayer.position);
            if (currentPage < pages.Count - 1)
            {
                currentPage++;
                UpdatePageText();
            }
        }

        private void PrevPage(UIMouseEvent evt, UIElement listeningElement)
        {
            SoundEngine.PlaySound(new SoundStyle("Opus/Assets/Audio/Pageturn"), Main.LocalPlayer.position);
            if (currentPage > 0)
            {
                currentPage--;
                UpdatePageText();
            }
        }

        private void UpdatePageText()
        {
            if (pages.Count > 0)
            {
                pageText.SetText(pages[currentPage]);
                pageNumberText.SetText($"Page {currentPage + 1} / {pages.Count}");
            }
            else
            {
                pageText.SetText("No content loaded.");
                pageNumberText.SetText("");
            }
        }


        // Old LoadText. Kept in case we need to backtrack.
        /*
        public void LoadText(string rawText)
        {
            pages.Clear();
            const int maxCharsPerPage = 2000;

            string[] words = rawText.Split(' ');
            string currentPageText = "";

            foreach (var word in words)
            {
                if ((currentPageText.Length + word.Length + 1) < maxCharsPerPage)
                {
                    currentPageText += word + " ";
                }
                else
                {
                    pages.Add(currentPageText.Trim());
                    currentPageText = word + " ";
                }
            }

            if (!string.IsNullOrWhiteSpace(currentPageText))
                pages.Add(currentPageText.Trim());

            currentPage = 0;
            UpdatePageText();
        }
        */

        public override void Update(GameTime gameTime)
        {
            if (Visible)
            {
                base.Update(gameTime);
            }

            if (Visible)
            {
                if (dragging)
                {
                    Vector2 mouse = new Vector2(Main.mouseX, Main.mouseY);
                    panel.Left.Set(mouse.X - offset.X, 0f);
                    panel.Top.Set(mouse.Y - offset.Y, 0f);
                    panel.Recalculate(); // Important to update its internal layout
                }
            }

            if (IsMouseHovering)
            {
                Main.LocalPlayer.mouseInterface = true;
            }
        }

        public void ToggleVisibility(string bookKey)
        {
            if (!Visible)
            {
                if (OpusBookRegistry.Exists(bookKey))
                {
                    OpenBook(bookKey);
                }
                else
                {
                    // Book not found, don’t force OpenBook
                    pages.Clear();
                    pages.Add("No book registered under this key.");
                    currentPage = 0;
                    UpdatePageText();
                    Visible = true;
                    return;
                }
            }

            Visible = !Visible;
        }


        public void Close(UIMouseEvent evt, UIElement listeningElement)
        {
            Visible = false;
        }
    }
    
    public class OpusReader : ModSystem
    {
        internal OpusBookUI bookUI;
        private UserInterface bookInterface;

        // Registry of all registered books
        private static readonly Dictionary<string, List<string>> books = new Dictionary<string, List<string>>();

        public static void RegisterBook(string bookKey, List<string> pages)
        {
            if (string.IsNullOrWhiteSpace(bookKey))
                throw new ArgumentException("Book key cannot be null or empty.", nameof(bookKey));

            books[bookKey] = pages ?? new List<string>();
        }

        public static List<string> GetBook(string bookKey)
        {
            return books.TryGetValue(bookKey, out var pages) ? pages : null;
        }

        public override void Load()
        {
            if (!Main.dedServ)
            {
                bookUI = new OpusBookUI();
                bookUI.Activate();
                bookInterface = new UserInterface();
                bookInterface.SetState(bookUI);
            }
        }

        public override void UpdateUI(GameTime gameTime)
        {
            if (!Main.dedServ && OpusBookUI.Visible)
                bookInterface?.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            int inventoryIndex = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Inventory"));
            if (inventoryIndex != -1 && OpusBookUI.Visible)
            {
                layers.Insert(inventoryIndex, new LegacyGameInterfaceLayer(
                    "Opus: Book UI",
                    () => { bookInterface.Draw(Main.spriteBatch, new GameTime()); return true; },
                    InterfaceScaleType.UI)
                );
            }
        }

        public void OpenBook(string bookKey)
        {
            if (OpusBookRegistry.Exists(bookKey))
            {
                bookUI.OpenBook(bookKey);
            }
        }

    }


}
