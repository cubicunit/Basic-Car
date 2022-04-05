using UnityEngine;

namespace BasicCarParking {
    public enum GEARBOX {
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
}
