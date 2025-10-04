using System.Collections.Generic;
using Opus.Content.Helpers;
using Opus.Content.OpusBook;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Opus.Content.Items
{
    public class WelcomeToOpus : OpusBookItem
    {
        public override string GetBookKey() => "WelcomeToOpus";
    }

    public class WelcomeBookLoading : ModSystem
    {
        public override void Load()
        {
            if (!Main.dedServ)
            {
                OpusBookRegistry.RegisterBook("WelcomeToOpus", new Dictionary<int, string>
                {
                    [0] = @"WELCOME TO OPUS
    By HoleCat",

                    [1] = @"Welcome to Opus! A small utility mod focused on practical applications
and ease of integration! This book will get you through all of the most
important things about Opus, including how to register a book just
like this one!.",
                    
                    [2] = @"First, make your item class derivative of the OpusBookItem class,
which defines characteristics consistent across most books expected.
Of course, you are free to override or modify it however you like it
for your books."
                });
            }
        }
}
}