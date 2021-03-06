﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprache;

namespace SpracheGameCore
{
    public class GameParser
    {
        public static Parser<string> Number = Parse.Digit.AtLeastOnce().Text().Token();

        public static Parser<Command> Command = Parse.Char('<').Then(_ => Parse.Char('>'))
                                                .Return(SpracheGameCore.Command.Between)
                                            .Or(Parse.Char('<')
                                                .Return(SpracheGameCore.Command.Less))
                                            .Or(Parse.Char('>')
                                                .Return(SpracheGameCore.Command.Greater))
                                            .Or(Parse.Char('=')
                                                .Return(SpracheGameCore.Command.Equal));        
        public static Parser<Play> Play =
           (from action in Command
            from value in Number
            select new Play(action, value, null))
        .Or(from firstValue in Number
            from action in Command
            from secondValue in Number
            select new Play(action, firstValue, secondValue));
    }
}
