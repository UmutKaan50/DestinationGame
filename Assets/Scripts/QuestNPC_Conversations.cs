//
// Copyright (c) Umut Kaan Özdemir. All rights reserved.
//

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace y01cu {
    public class QuestNPC_Conversations {
        private List<string> levelOneInitialConversation = new List<string> {
            "Hi, adventurer!",
            "Welcome to Destination!",
            "I'm Timur, the \"Deadly Puzzle\" master.",
            "In order to get out of here, you need to solve the puzzles that I'll enter you in.",
            "While you're facing with puzzles you'll be nailed to the ground, won't be able to move and attack.",
            "They're called \"Deadly Puzzles\"",
            "Make sure you're ready and let me know before you enter a Deadly Puzzle!",
            "But since you're just starting your journey this one will not be a Deadly one.",
            "Train yourself well with it, while you can...",
            "Alright, enough talking. Are you ready to face it?"
        };

        private List<string> levelOneNormalConversation = new List<string> {
            "Hi again, adventurer!",
            "Are you ready this time?"
        };

        private List<string> levelTwoConversation = new List<string> {
            "We're starting with deadly puzzles.",
            "Solve the puzzle as fast as you can.",
            "Since skeletons will come an attack you otherwie.",
            "Are you ready?"
        };

        public List<string> GetLevelOneInitialConversation() {
            return levelOneInitialConversation;
        }

        public List<string> GetLevelOneNormalConversation() {
            return levelOneNormalConversation;
        }

        public List<string> GetLevelTwoConversation() {
            return levelTwoConversation;
        }
    }
}