/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID ENEMY_BAT1 = 2768764968U;
        static const AkUniqueID ENEMY_BAT2 = 2768764971U;
        static const AkUniqueID ENEMY_BLOB = 2582092671U;
        static const AkUniqueID ENEMY_GHOST = 1974222287U;
        static const AkUniqueID ENEMY_PLANT = 1098871033U;
        static const AkUniqueID GAME_START = 733168346U;
        static const AkUniqueID PLAYER_DIE = 1542330065U;
        static const AkUniqueID PLAYER_GRAB_GEM = 52976355U;
        static const AkUniqueID PLAYER_LAND = 3629196698U;
        static const AkUniqueID PLAYER_RELEASE = 590715968U;
        static const AkUniqueID PLAYER_TOUCH = 2993297310U;
        static const AkUniqueID PLAYER_WINGS = 3239602367U;
        static const AkUniqueID POWERUP_END = 3584997169U;
        static const AkUniqueID POWERUP_START = 676410594U;
        static const AkUniqueID TRAP_ARROWS = 40631397U;
        static const AkUniqueID TRAP_PLATFORM_CLOSE = 3550371055U;
        static const AkUniqueID TRAP_PLATFORM_OPEN = 4293073065U;
        static const AkUniqueID TRAP_SPIKES = 584352822U;
        static const AkUniqueID TRAP_TENTACLES = 3064100384U;
        static const AkUniqueID UI_CLICK = 2249769530U;
        static const AkUniqueID UI_HOME = 12137035U;
        static const AkUniqueID UI_INGAME_HAND = 1170205103U;
        static const AkUniqueID UI_INGAME_HIGHSCORE = 104469174U;
        static const AkUniqueID UI_INGAME_SCORE = 2967661922U;
        static const AkUniqueID UI_INGAME_SCORE_OFF = 157712808U;
        static const AkUniqueID UI_INGAME_WARNING = 2850178896U;
        static const AkUniqueID UI_MENU_ENTER = 3218841108U;
        static const AkUniqueID UI_MENU_START = 2961968006U;
        static const AkUniqueID UI_PAUSE_OFF = 952138190U;
        static const AkUniqueID UI_PAUSE_ON = 1731872944U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace INGAME
        {
            static const AkUniqueID GROUP = 984691642U;

            namespace STATE
            {
                static const AkUniqueID GHOST = 4023194814U;
                static const AkUniqueID PAUSE = 3092587493U;
                static const AkUniqueID PLAY = 1256202815U;
                static const AkUniqueID POWERUP = 3950429679U;
                static const AkUniqueID SCORE = 2398231425U;
            } // namespace STATE
        } // namespace INGAME

        namespace MENU_SCREEN
        {
            static const AkUniqueID GROUP = 2292459139U;

            namespace STATE
            {
                static const AkUniqueID CONFIG = 3469696991U;
                static const AkUniqueID CREDITS = 2201105581U;
                static const AkUniqueID MENU = 2607556080U;
            } // namespace STATE
        } // namespace MENU_SCREEN

        namespace MUSIC_PAUSE
        {
            static const AkUniqueID GROUP = 2895066125U;

            namespace STATE
            {
                static const AkUniqueID PAUSE_OFF = 3967028551U;
                static const AkUniqueID PAUSE_ON = 3537680115U;
            } // namespace STATE
        } // namespace MUSIC_PAUSE

        namespace MUTE
        {
            static const AkUniqueID GROUP = 2974103762U;

            namespace STATE
            {
                static const AkUniqueID MUTE_FX = 1932282383U;
                static const AkUniqueID MUTE_MX = 1814839050U;
            } // namespace STATE
        } // namespace MUTE

        namespace PLAYER
        {
            static const AkUniqueID GROUP = 1069431850U;

            namespace STATE
            {
                static const AkUniqueID ALIVE = 655265632U;
                static const AkUniqueID DEAD = 2044049779U;
            } // namespace STATE
        } // namespace PLAYER

        namespace SCREEN
        {
            static const AkUniqueID GROUP = 804877345U;

            namespace STATE
            {
                static const AkUniqueID INGAME = 984691642U;
                static const AkUniqueID MENU = 2607556080U;
            } // namespace STATE
        } // namespace SCREEN

    } // namespace STATES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID DISTANCE_ENEMY_BAT1 = 381081086U;
        static const AkUniqueID DISTANCE_ENEMY_BAT2 = 381081085U;
        static const AkUniqueID DISTANCE_ENEMY_BLOB = 262799029U;
        static const AkUniqueID DISTANCE_ENEMY_GHOST = 2622755353U;
        static const AkUniqueID DISTANCE_ENEMY_PLANT = 1034163099U;
        static const AkUniqueID DISTANCE_TRAP_MOVING_PLATFORM = 3732534371U;
        static const AkUniqueID DISTANCE_TRAP_SPIKES = 2411429840U;
        static const AkUniqueID DISTANCE_TRAP_TENTACLE = 2145236951U;
        static const AkUniqueID FLOOR_NUMBER = 1884276375U;
        static const AkUniqueID POWERUP_STATE = 709965785U;
        static const AkUniqueID VOLUME_MUSIC = 3891337659U;
        static const AkUniqueID VOLUME_SFX = 3673881719U;
    } // namespace GAME_PARAMETERS

    namespace TRIGGERS
    {
        static const AkUniqueID DIE = 445985469U;
        static const AkUniqueID HIGHSCORE = 2808377621U;
    } // namespace TRIGGERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID INGAME = 984691642U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID SFX = 393239870U;
        static const AkUniqueID UI = 1551306167U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
