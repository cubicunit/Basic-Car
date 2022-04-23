using UnityEngine;

namespace BasicCarParking {
    public enum GAMESTATE {
        PAUSE,
        START
    }

    public enum CAMERAMODE {
        PLAY,
        SPECTATE
    }

    public enum GEARTYPE {
        PARK = -2,
        REVERSE = -1,
        NEUTRAL = 0,
        DRIVE = 1
    }

    public enum GEARDIR {
        UP = 1,
        DOWN = -1
    }

    public enum VIEWTYPE {
        FIRST_PERSON_VIEW = 0,
        TOP_DOWN_VIEW = 1,
        THIRD_PERSON_VIEW = 2
    }

    public enum LOOKDIR {
        LEFT = -1,
        CENTER = 0,
        RIGHT = 1
    }

    public sealed class GameData {
        private static GameData instance = null;  
        private GameData() {}  

        public int level = 1;
        public static GameData Instance {  
            get {  
                if (instance == null) {  
                    instance = new GameData();  
                }  
                return instance;  
            }  
        } 

        private int test {get; set;}
    }
}
