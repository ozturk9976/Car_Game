using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SYS
{
    public enum RotateType
    {
        Yoyo,
        Restart
    }

    public enum Degree
    {
        Degree_0,
        Degree_right_45,
        Degree_right_90,
        Degree_right_135,
        Degree_right_180,
        Degree_left_135,
        Degree_left_90,
        Degree_left_45,
    }
    public enum Floor
    {
        plane,
        first_floor,
        second_floor
    }
    public enum WHEEL_TYPE
    {
        FRONT_LEFT,
        FRONT_RIGHT,
        REAR_LEFT,
        REAR_RIGHT
    }

    public enum WHEEL_GROUP
    {
        FRONT,
        REAR
    }

}
namespace MENU_ENUMS
{
    public enum PAGES
    {
        #region Menus
        MAIN_MENU,
        GARAGE_MENU,
        TUNING_MENU,
        OPTIONS_MENU,
        #endregion

        #region Panels
        REFUEL_PANEL
        #endregion        
    }
}
namespace GAMEPLAY_ENUMS
{
    public enum CAR_SENSORS
    {
        FRONT_LEFT_CAR_SENSOR,
        REAR_RIGHT_CAR_SENSOR,

        FRONT_RIGHT_CAR_SENSOR,
        REAR_LEFT_CAR_SENSOR,
    }
    public enum BLINK_SENSOR
    {
        #region Front
        BLINK_FRONT_SENSOR,
        BLINK_FRONT_LEFT_SENSOR,
        BLINK_FRONT_RIGHT_SENSOR,

        #endregion

        #region Rear
        BLINK_REAR_SENSOR,
        BLINK_REAR_RIGHT_SENSOR,
        BLINK_REAR_LEFT_SENSOR,
        #endregion
    }

    public enum DISTANCE_TEXT_FOR_SENSOR
    {
        //These values can be set from sensor images from UI
        BLINK_FRONT_CAR_SENSOR_TEXT,
        BLINK_FRONT_CAR_SENSOR_2_TEXT,
        BLINK_FRONT_LEFT_SENSOR_TEXT,
        BLINK_FRONT_LEFT_SENSOR_2_TEXT,
        BLINK_FRONT_RIGHT_SENSOR_TEXT,
        BLINK_FRONT_RIGHT_SENSOR_2_TEXT,

        BLINK_REAR_SENSOR_TEXT,
        BLINK_REAR_SENSOR_2_TEXT,
        BLINK_REAR_RIGHT_SENSOR_TEXT,
        BLINK_REAR_RIGHT_SENSOR_2_TEXT,
        BLINK_REAR_LEFT_SENSOR_TEXT,
        BLINK_REAR_LEFT_SENSOR__TEXT2
    }
}

namespace SETTING_ENUMS
{
    public enum ShadowDistance
    {
        low,
        mid,
        high
    }
    public enum ResulotionScale
    {
        low,
        mid,
        high
    }
}
