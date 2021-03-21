/*! \class Globals
 *  \brief Contains some constant variables values.
 */
public class Globals {

    public const float CH_JUMP_DURATION = 0.833f;                           //!< Character jump animation duration.
    public const float CH_SLIDE_DURATION = 1f;                              //!< Character slide duration.
    public const float CH_DODGE_DURATION = 0.2f;                            //!< Character dodge duration.

    public const float CH_COLLIDER_NORMAL_Y_POS = 1.1f;                     //!< Collider y position when running.
    public const float CH_COLLIDER_SLIDING_Y_POS = 0.5f;                    //!< Collider y position when sliding.     

    public const float CH_COLLIDER_NORMAL_HEIGHT = 1.5f;                     //!< Collider y position when running.
    public const float CH_COLLIDER_SLIDING_HEIGHT = 1f;                      //!< Collider y position when sliding.    

    public const float MD_RECYCLE_POS_OFFSET = 25f;                         //!< Distance behind the camera to recycle the modules.
        
    public const float SP_POSITION_FROM_PLAYER = 115f;                      //!< Minimum distance from player to set the position of spawned objects.
    public const float SP_NO_COINS_CHANCE = 0.3f;                           //!< Percentage chance to don't spawn coins before the obstacle.

    public const float LINE01_X_POS = -2.75f;                               //!< Left running line.
    public const float LINE02_X_POS = 0f;                                   //!< Middle running line.
    public const float LINE03_X_POS = 2.75f;                                //!< Right running line.

    public const float IT_MAGNETIZED_ITEM_SPEED = 15f;                      //!< Movement speed of the items affect by the magnet effect.

    public const float OBS_LIZARD_JUMP_TRIGGER_DIST = 20f;                  //!< Distance from the player character to trigger the lizard jump.
    public const float OBS_LIZARD_JUMP_IMPULSE_DURATION = 0.5f;             //!< Animation jump impulse delay.
    public const float OBS_LIZARD_JUMP_DURATION = 0.7f;                     //!< Lizard obstacle jump animation duration.
}
