using System;
using System.Collections.Generic;
using System.Text;

namespace Thebes
{
    public static class GameSettings
    {
        public static List<Place> Places { get; set; }
        public static Place StartingPlace { get; set; }
        private static int[,] Distances { get; set; }
        private static Dictionary<int, Dictionary<int, int>> TimeWheel { get; set; }
        public static List<Card> Cards { get; set; }

        public static int GetDistance(Place from, Place to)
        {
            return Distances[from.Index, to.Index];
        }

        public static int DugTokenCount(int knowledge, int weeks)
        {
            return TimeWheel[knowledge][weeks];
        }
        public static void LoadFromFile()
        {
            throw new NotImplementedException();
        }

        public static void Initialize() // temporary, will be replaced with json config file
        {
            List<Token> CreateTokenList(DigSite digSite, int one, int two, int three, int four, int five, int six, int seven, DigSite knowledgeDigSite)
            {
                List<Token> tokenList = new List<Token>();

                // Specialized Knowledge
                tokenList.Add(new SpecializedKnowledgeToken("TODO", digSite, 1, knowledgeDigSite));

                // General Knowledge
                tokenList.Add(new GeneralKnowledgeToken("TODO", digSite, 1));

                // Dirt
                for (int i = 0; i < 16; i++)
                {
                    tokenList.Add(new DirtToken("TODO", digSite));
                }

                // Artifacts
                for (int i = 0; i < one; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 1, "TODO"));
                }

                for (int i = 0; i < two; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 2, "TODO"));
                }

                for (int i = 0; i < three; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 3, "TODO"));
                }

                for (int i = 0; i < four; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 4, "TODO"));
                }

                for (int i = 0; i < five; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 5, "TODO"));
                }

                for (int i = 0; i < six; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 6, "TODO"));
                }

                for (int i = 0; i < seven; i++)
                {
                    tokenList.Add(new ArtifactToken("TODO", digSite, 7, "TODO"));
                }

                if (tokenList.Count != 31)
                {
                    throw new InvalidOperationException();
                }
                return tokenList;
            }

      
            // PLACES
            // Universities
            Places = new List<Place>();
            University london = new University("London", 0);
            Places.Add(london);

            University paris = new University("Paris", 1);
            Places.Add(paris);

            University berlin = new University("Berlin", 2);
            Places.Add(berlin);

            University vienna = new University("Vienna", 3);
            Places.Add(vienna);

            University moscow = new University("Moscow", 4);
            Places.Add(moscow);

            University rome = new University("Rome", 5);
            Places.Add(rome);

            // Card chagne places
            CardChangePlace warsaw = new CardChangePlace("Warsaw", 6);
            Places.Add(warsaw);

            // Digsites
            DigSite greece = new DigSite("Greece", 7);
            DigSite mesopotamia = new DigSite("Mesopotamia", 8);
            DigSite egypt = new DigSite("Egypt", 9);
            DigSite crete = new DigSite("Crete", 10);
            DigSite palestine = new DigSite("Palestine", 11);

            // Adding tokens to digsites
            greece.Tokens = CreateTokenList(greece, 4, 3, 2, 1, 2, 1, 0, crete);
            Places.Add(greece);

            mesopotamia.Tokens = CreateTokenList(mesopotamia, 5, 0, 3, 3, 2, 0, 0, palestine);
            Places.Add(mesopotamia);

            egypt.Tokens = CreateTokenList(egypt, 4, 2, 3, 2, 1, 1, 0, mesopotamia);
            Places.Add(egypt);

            crete.Tokens = CreateTokenList(crete, 3, 2, 4, 3, 1, 0, 0, greece);
            Places.Add(crete);

            palestine.Tokens = CreateTokenList(palestine, 5, 3, 1, 1, 1, 1, 1, egypt);
            Places.Add(palestine);


            StartingPlace = warsaw;

            // DISTANCES
            Distances = new int[12, 12];

            Distances[0, 0] = 0;
            Distances[0, 1] = 1;
            Distances[0, 2] = 1;
            Distances[0, 3] = 2;
            Distances[0, 4] = 3;
            Distances[0, 5] = 2;
            Distances[0, 6] = 2;
            Distances[0, 7] = 3;
            Distances[0, 8] = 4;
            Distances[0, 9] = 4;
            Distances[0, 10] = 3;
            Distances[0, 11] = 4;

            Distances[1, 0] = 1;
            Distances[1, 1] = 0;
            Distances[1, 2] = 1;
            Distances[1, 3] = 1;
            Distances[1, 4] = 3;
            Distances[1, 5] = 1;
            Distances[1, 6] = 2;
            Distances[1, 7] = 2;
            Distances[1, 8] = 3;
            Distances[1, 9] = 3;
            Distances[1, 10] = 2;
            Distances[1, 11] = 3;

            Distances[2, 0] = 1;
            Distances[2, 1] = 1;
            Distances[2, 2] = 0;
            Distances[2, 3] = 2;
            Distances[2, 4] = 2;
            Distances[2, 5] = 2;
            Distances[2, 6] = 1;
            Distances[2, 7] = 2;
            Distances[2, 8] = 3;
            Distances[2, 9] = 4;
            Distances[2, 10] = 3;
            Distances[2, 11] = 3;

            Distances[3, 0] = 2;
            Distances[3, 1] = 1;
            Distances[3, 2] = 2;
            Distances[3, 3] = 0;
            Distances[3, 4] = 2;
            Distances[3, 5] = 1;
            Distances[3, 6] = 1;
            Distances[3, 7] = 2;
            Distances[3, 8] = 3;
            Distances[3, 9] = 3;
            Distances[3, 10] = 2;
            Distances[3, 11] = 3;

            Distances[4, 0] = 3;
            Distances[4, 1] = 3;
            Distances[4, 2] = 2;
            Distances[4, 3] = 2;
            Distances[4, 4] = 0;
            Distances[4, 5] = 3;
            Distances[4, 6] = 1;
            Distances[4, 7] = 2;
            Distances[4, 8] = 3;
            Distances[4, 9] = 4;
            Distances[4, 10] = 3;
            Distances[4, 11] = 4;

            Distances[5, 0] = 1;
            Distances[5, 1] = 1;
            Distances[5, 2] = 2;
            Distances[5, 3] = 1;
            Distances[5, 4] = 3;
            Distances[5, 5] = 0;
            Distances[5, 6] = 2;
            Distances[5, 7] = 1;
            Distances[5, 8] = 2;
            Distances[5, 9] = 2;
            Distances[5, 10] = 1;
            Distances[5, 11] = 2;

            Distances[6, 0] = 2;
            Distances[6, 1] = 2;
            Distances[6, 2] = 1;
            Distances[6, 3] = 1;
            Distances[6, 4] = 1;
            Distances[6, 5] = 2;
            Distances[6, 6] = 0;
            Distances[6, 7] = 1;
            Distances[6, 8] = 2;
            Distances[6, 9] = 3;
            Distances[6, 10] = 2;
            Distances[6, 11] = 3;

            Distances[7, 0] = 3;
            Distances[7, 1] = 2;
            Distances[7, 2] = 2;
            Distances[7, 3] = 2;
            Distances[7, 4] = 2;
            Distances[7, 5] = 1;
            Distances[7, 6] = 1;
            Distances[7, 7] = 0;
            Distances[7, 8] = 1;
            Distances[7, 9] = 2;
            Distances[7, 10] = 1;
            Distances[7, 11] = 2;

            Distances[8, 0] = 4;
            Distances[8, 1] = 3;
            Distances[8, 2] = 3;
            Distances[8, 3] = 3;
            Distances[8, 4] = 3;
            Distances[8, 5] = 2;
            Distances[8, 6] = 2;
            Distances[8, 7] = 1;
            Distances[8, 8] = 0;
            Distances[8, 9] = 2;
            Distances[8, 10] = 2;
            Distances[8, 11] = 1;

            Distances[9, 0] = 4;
            Distances[9, 1] = 3;
            Distances[9, 2] = 4;
            Distances[9, 3] = 3;
            Distances[9, 4] = 4;
            Distances[9, 5] = 2;
            Distances[9, 6] = 3;
            Distances[9, 7] = 2;
            Distances[9, 8] = 2;
            Distances[9, 9] = 0;
            Distances[9, 10] = 1;
            Distances[9, 11] = 1;

            Distances[10, 0] = 3;
            Distances[10, 1] = 2;
            Distances[10, 2] = 3;
            Distances[10, 3] = 2;
            Distances[10, 4] = 3;
            Distances[10, 5] = 1;
            Distances[10, 6] = 2;
            Distances[10, 7] = 1;
            Distances[10, 8] = 2;
            Distances[10, 9] = 1;
            Distances[10, 10] = 0;
            Distances[10, 11] = 1;

            Distances[11, 0] = 4;
            Distances[11, 1] = 3;
            Distances[11, 2] = 3;
            Distances[11, 3] = 3;
            Distances[11, 4] = 4;
            Distances[11, 5] = 2;
            Distances[11, 6] = 3;
            Distances[11, 7] = 2;
            Distances[11, 8] = 2;
            Distances[11, 9] = 1;
            Distances[11, 10] = 1;
            Distances[11, 11] = 0;

            // TIMEWHEEL
            TimeWheel = new Dictionary<int, Dictionary<int, int>>();

            TimeWheel.Add(1, new Dictionary<int, int>
            {
                {1, 0},
                {2, 0},
                {3, 0},
                {4, 0},
                {5, 0},
                {6, 0},
                {7, 0},
                {8, 1},
                {9, 1},
                {10, 1},
                {11, 1},
                {12, 2},
            });

            TimeWheel.Add(2, new Dictionary<int, int>
            {
                {1, 0},
                {2, 0},
                {3, 0},
                {4, 0},
                {5, 0},
                {6, 1},
                {7, 1},
                {8, 1},
                {9, 1},
                {10, 2},
                {11, 2},
                {12, 2},
            });

            TimeWheel.Add(3, new Dictionary<int, int>
            {
                {1, 0},
                {2, 0},
                {3, 1},
                {4, 1},
                {5, 1},
                {6, 2},
                {7, 2},
                {8, 2},
                {9, 3},
                {10, 3},
                {11, 3},
                {12, 4},
            });

            TimeWheel.Add(4, new Dictionary<int, int>
            {
                {1, 0},
                {2, 1},
                {3, 1},
                {4, 2},
                {5, 2},
                {6, 3},
                {7, 3},
                {8, 4},
                {9, 4},
                {10, 4},
                {11, 5},
                {12, 5},
            });

            TimeWheel.Add(5, new Dictionary<int, int>
            {
                {1, 1},
                {2, 1},
                {3, 2},
                {4, 3},
                {5, 3},
                {6, 4},
                {7, 4},
                {8, 5},
                {9, 5},
                {10, 6},
                {11, 7},
                {12, 8},
            });

            TimeWheel.Add(6, new Dictionary<int, int>
            {
                {1, 1},
                {2, 2},
                {3, 3},
                {4, 4},
                {5, 4},
                {6, 5},
                {7, 5},
                {8, 5},
                {9, 6},
                {10, 6},
                {11, 7},
                {12, 8},
            });

            TimeWheel.Add(7, new Dictionary<int, int>
            {
                {1, 1},
                {2, 2},
                {3, 4},
                {4, 4},
                {5, 5},
                {6, 5},
                {7, 6},
                {8, 6},
                {9, 7},
                {10, 8},
                {11, 8},
                {12, 9},
            });

            TimeWheel.Add(8, new Dictionary<int, int>
            {
                {1, 1},
                {2, 3},
                {3, 4},
                {4, 5},
                {5, 5},
                {6, 6},
                {7, 6},
                {8, 7},
                {9, 8},
                {10, 9},
                {11, 9},
                {12, 10},
            });

            TimeWheel.Add(9, new Dictionary<int, int>
            {
                {1, 2},
                {2, 3},
                {3, 5},
                {4, 5},
                {5, 6},
                {6, 6},
                {7, 7},
                {8, 8},
                {9, 9},
                {10, 9},
                {11, 10},
                {12, 10},
            });

            TimeWheel.Add(10, new Dictionary<int, int>
            {
                {1, 2},
                {2, 3},
                {3, 5},
                {4, 5},
                {5, 6},
                {6, 7},
                {7, 8},
                {8, 9},
                {9, 9},
                {10, 10},
                {11, 10},
                {12, 10},
            });

            TimeWheel.Add(11, new Dictionary<int, int>
            {
                {1, 2},
                {2, 4},
                {3, 5},
                {4, 6},
                {5, 6},
                {6, 7},
                {7, 8},
                {8, 9},
                {9, 10},
                {10, 10},
                {11, 10},
                {12, 10},
            });

            TimeWheel.Add(12, new Dictionary<int, int>
            {
                {1, 3},
                {2, 4},
                {3, 5},
                {4, 6},
                {5, 7},
                {6, 8},
                {7, 9},
                {8, 10},
                {9, 10},
                {10, 10},
                {11, 10},
                {12, 10},
            });

            //
            //
            // CARDS
            //
            //

            Cards = new List<Card>();

            // EXHIBITIONS
            // small

            Cards.Add(new ExhibitionCard("TODO", london, 3, 4, new Dictionary<DigSite, int> {
                { greece, 1},
                { egypt, 2 }
            }));

            Cards.Add(new ExhibitionCard("TODO", moscow, 3, 4, new Dictionary<DigSite, int> {
                { mesopotamia, 1},
                { crete, 2 }
            }));

            Cards.Add(new ExhibitionCard("TODO", vienna, 3, 4, new Dictionary<DigSite, int> {
                { egypt, 1},
                { palestine, 2 }
            }));

            Cards.Add(new ExhibitionCard("TODO", paris, 3, 4, new Dictionary<DigSite, int> {
                { palestine, 1},
                { mesopotamia, 2 }
            }));

            Cards.Add(new ExhibitionCard("TODO", berlin, 3, 4, new Dictionary<DigSite, int> {
                { crete, 1},
                { greece, 2 }
            }));

            // large

            Cards.Add(new ExhibitionCard("TODO", berlin, 4, 5, new Dictionary<DigSite, int> {
                { palestine, 1},
                { mesopotamia, 2 },
                { crete, 3 }
            }));

            Cards.Add(new ExhibitionCard("TODO", paris, 4, 5, new Dictionary<DigSite, int> {
                { greece, 1},
                { egypt, 2 },
                { palestine, 3 }
            }));

            Cards.Add(new ExhibitionCard("TODO", london, 4, 5, new Dictionary<DigSite, int> {
                { crete, 1},
                { greece, 2 },
                { egypt, 3 }
            }));

            Cards.Add(new ExhibitionCard("TODO", vienna, 4, 5, new Dictionary<DigSite, int> {
                { mesopotamia, 1},
                { crete, 2 },
                { greece, 3 }
            }));

            Cards.Add(new ExhibitionCard("TODO", moscow, 4, 5, new Dictionary<DigSite, int> {
                { egypt, 1},
                { palestine, 2 },
                { mesopotamia, 3 }
            }));


            //
            // Special cards
            //

            // Assistents
            Cards.Add(new AssistentCard("TODO", berlin, 2));
            Cards.Add(new AssistentCard("TODO", paris, 2));
            Cards.Add(new AssistentCard("TODO", paris, 2));
            Cards.Add(new AssistentCard("TODO", rome, 2));
            Cards.Add(new AssistentCard("TODO", vienna, 2));

            // Shovels
            Cards.Add(new ShovelCard("TODO", london, 3));
            Cards.Add(new ShovelCard("TODO", london, 3));
            Cards.Add(new ShovelCard("TODO", moscow, 3));
            Cards.Add(new ShovelCard("TODO", moscow, 3));
            Cards.Add(new ShovelCard("TODO", rome, 3));
            Cards.Add(new ShovelCard("TODO", rome, 3));

            // zeppelins
            Cards.Add(new ZeppelinCard("TODO", rome, 1));
            Cards.Add(new ZeppelinCard("TODO", vienna, 1));

            // cars
            Cards.Add(new CarCard("TODO", rome, 1));
            Cards.Add(new CarCard("TODO", moscow, 1));

            // permission
            Cards.Add(new SpecialPermissionCard("TODO", moscow, 3));
            Cards.Add(new SpecialPermissionCard("TODO", london, 3));

            // congress cards
            Cards.Add(new CongressCard("TODO", london, 2));
            Cards.Add(new CongressCard("TODO", moscow, 2));
            Cards.Add(new CongressCard("TODO", moscow, 2));
            Cards.Add(new CongressCard("TODO", paris, 2));
            Cards.Add(new CongressCard("TODO", paris, 2));
            Cards.Add(new CongressCard("TODO", vienna, 2));
            Cards.Add(new CongressCard("TODO", vienna, 2));
            Cards.Add(new CongressCard("TODO", berlin, 2));
            Cards.Add(new CongressCard("TODO", berlin, 2));

            // general knowledge

            Cards.Add(new GeneralKnowledgeCard("TODO", berlin, 6, 2));
            Cards.Add(new GeneralKnowledgeCard("TODO", moscow, 6, 2));
            Cards.Add(new GeneralKnowledgeCard("TODO", london, 6, 2));
            Cards.Add(new GeneralKnowledgeCard("TODO", paris, 6, 2));

            Cards.Add(new GeneralKnowledgeCard("TODO", berlin, 3, 1));
            Cards.Add(new GeneralKnowledgeCard("TODO", rome, 3, 1));
            Cards.Add(new GeneralKnowledgeCard("TODO", vienna, 3, 1));
            Cards.Add(new GeneralKnowledgeCard("TODO", paris, 3, 1));

            // special knowledge mesopotamy
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 1, 1, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 1, 1, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 1, 1, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 1, 1, mesopotamia));
          
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 2, 2, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 2, 2, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 2, 2, mesopotamia));
                    
            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 4, 3, mesopotamia));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 4, 3, mesopotamia));

            Cards.Add(new RumorsCard("TODO", berlin, 1, 2, mesopotamia));

            // special knowledge greece
            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 1, 1, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 1, 1, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 1, 1, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 1, 1, greece));

            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 2, 2, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 2, 2, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 2, 2, greece));

            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 4, 3, greece));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 4, 3, greece));

            Cards.Add(new RumorsCard("TODO", moscow, 1, 2, greece));

            // special knowledge crete
            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 1, 1, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 1, 1, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 1, 1, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 1, 1, crete));

            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 2, 2, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 2, 2, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 2, 2, crete));

            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 4, 3, crete));
            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 4, 3, crete));

            Cards.Add(new RumorsCard("TODO", paris, 1, 2, crete));

            // special knowledge egypt
            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 1, 1, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 1, 1, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 1, 1, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 1, 1, egypt));

            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 2, 2, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 2, 2, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 2, 2, egypt));

            Cards.Add(new SpecializedKnowledgeCard("TODO", moscow, 4, 3, egypt));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 4, 3, egypt));

            Cards.Add(new RumorsCard("TODO", rome, 1, 2, egypt));

            // special knowledge palestine
            Cards.Add(new SpecializedKnowledgeCard("TODO", rome, 1, 1, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 1, 1, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 1, 1, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", vienna, 1, 1, palestine));

            Cards.Add(new SpecializedKnowledgeCard("TODO", berlin, 2, 2, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 2, 2, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 2, 2, palestine));

            Cards.Add(new SpecializedKnowledgeCard("TODO", paris, 4, 3, palestine));
            Cards.Add(new SpecializedKnowledgeCard("TODO", london, 4, 3, palestine));

            Cards.Add(new RumorsCard("TODO", vienna, 1, 2, palestine));
        }
    }
}