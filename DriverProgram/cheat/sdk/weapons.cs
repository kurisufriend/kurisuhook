﻿using System;
using System.Collections.Generic;
using System.Text;

namespace recode.sdk
{
	public static class weapons
	{
		public enum knifeDefinitionIndex : Int32
		{
			WEAPON_KNIFE = 42,
			WEAPON_KNIFE_T = 59,
			KNIFE_BAYONET = 500,
			KNIFE_CSS = 503,
			KNIFE_FLIP = 505,
			KNIFE_GUT = 506,
			KNIFE_KARAMBIT = 507,
			KNIFE_M9_BAYONET = 508,
			KNIFE_TACTICAL = 509,
			KNIFE_FALCHION = 512,
			KNIFE_BOWIE = 514,
			KNIFE_BUTTERFLY = 515,
			KNIFE_PUSH = 516,
			KNIFE_CORD = 517,
			KNIFE_CANIS = 518,
			KNIFE_URSUS = 519,
			KNIFE_GYPSY = 520,
			KNIFE_OUTDOOR = 521,
			KNIFE_STILETTO = 522,
			KNIFE_WIDOWMAKER = 523,
			KNIFE_SKELETON = 525
		}
		public static string[] knifidArr = Enum.GetNames(typeof(knifeDefinitionIndex));
		public static Array knifeidArrVals = Enum.GetValues(typeof(knifeDefinitionIndex));
		public enum knifestuff : Int32
		{
			bayonet = 0,
			classic = 1,
			flip = 2,
			gut = 3,
			karambit = 4,
			m9_bayonet = 5,
			huntsman = 6,
			falchion = 7,
			bowie = 8,
			butterfly = 9,
			shadow_daggers = 10,
			paracord = 11,
			survival = 12,
			ursus = 13,
			navaja = 14,
			nomad = 15,
			stiletto = 16,
			talon = 17,
			skeleton = 18
		}
		public static string[] knifeArr = Enum.GetNames(typeof(knifestuff));
		public static Array knifeArrVals = Enum.GetValues(typeof(knifestuff));
		public enum ClassID : Int32
		{
			CTestTraceline = 223,
			CTEWorldDecal = 224,
			CTESpriteSpray = 221,
			CTESprite = 220,
			CTESparks = 219,
			CTESmoke = 218,
			CTEShowLine = 216,
			CTEProjectedDecal = 213,
			CFEPlayerDecal = 71,
			CTEPlayerDecal = 212,
			CTEPhysicsProp = 209,
			CTEParticleSystem = 208,
			CTEMuzzleFlash = 207,
			CTELargeFunnel = 205,
			CTEKillPlayerAttachments = 204,
			CTEImpact = 203,
			CTEGlowSprite = 202,
			CTEShatterSurface = 215,
			CTEFootprintDecal = 199,
			CTEFizz = 198,
			CTEExplosion = 196,
			CTEEnergySplash = 195,
			CTEEffectDispatch = 194,
			CTEDynamicLight = 193,
			CTEDecal = 191,
			CTEClientProjectile = 190,
			CTEBubbleTrail = 189,
			CTEBubbles = 188,
			CTEBSPDecal = 187,
			CTEBreakModel = 186,
			CTEBloodStream = 185,
			CTEBloodSprite = 184,
			CTEBeamSpline = 183,
			CTEBeamRingPoint = 182,
			CTEBeamRing = 181,
			CTEBeamPoints = 180,
			CTEBeamLaser = 179,
			CTEBeamFollow = 178,
			CTEBeamEnts = 177,
			CTEBeamEntPoint = 176,
			CTEBaseBeam = 175,
			CTEArmorRicochet = 174,
			CTEMetalSparks = 206,
			CSteamJet = 167,
			CSmokeStack = 157,
			DustTrail = 275,
			CFireTrail = 74,
			SporeTrail = 281,
			SporeExplosion = 280,
			RocketTrail = 278,
			SmokeTrail = 279,
			CPropVehicleDriveable = 144,
			ParticleSmokeGrenade = 277,
			CParticleFire = 116,
			MovieExplosion = 276,
			CTEGaussExplosion = 201,
			CEnvQuadraticBeam = 66,
			CEmbers = 55,
			CEnvWind = 70,
			CPrecipitation = 137,
			CPrecipitationBlocker = 138,
			CBaseTempEntity = 18,
			NextBotCombatCharacter = 0,
			CEconWearable = 54,
			CBaseAttributableItem = 4,
			CEconEntity = 53,
			CWeaponXM1014 = 272,
			CWeaponTaser = 267,
			CTablet = 171,
			CSnowball = 158,
			CSmokeGrenade = 155,
			CWeaponShield = 265,
			CWeaponSG552 = 263,
			CSensorGrenade = 151,
			CWeaponSawedoff = 259,
			CWeaponNOVA = 255,
			CIncendiaryGrenade = 99,
			CMolotovGrenade = 112,
			CMelee = 111,
			CWeaponM3 = 247,
			CKnifeGG = 108,
			CKnife = 107,
			CHEGrenade = 96,
			CFlashbang = 77,
			CFists = 76,
			CWeaponElite = 238,
			CDecoyGrenade = 47,
			CDEagle = 46,
			CWeaponUSP = 271,
			CWeaponM249 = 246,
			CWeaponUMP45 = 270,
			CWeaponTMP = 269,
			CWeaponTec9 = 268,
			CWeaponSSG08 = 266,
			CWeaponSG556 = 264,
			CWeaponSG550 = 262,
			CWeaponScout = 261,
			CWeaponSCAR20 = 260,
			CSCAR17 = 149,
			CWeaponP90 = 258,
			CWeaponP250 = 257,
			CWeaponP228 = 256,
			CWeaponNegev = 254,
			CWeaponMP9 = 253,
			CWeaponMP7 = 252,
			CWeaponMP5Navy = 251,
			CWeaponMag7 = 250,
			CWeaponMAC10 = 249,
			CWeaponM4A1 = 248,
			CWeaponHKP2000 = 245,
			CWeaponGlock = 244,
			CWeaponGalilAR = 243,
			CWeaponGalil = 242,
			CWeaponG3SG1 = 241,
			CWeaponFiveSeven = 240,
			CWeaponFamas = 239,
			CWeaponBizon = 234,
			CWeaponAWP = 232,
			CWeaponAug = 231,
			CAK47 = 1,
			CWeaponCSBaseGun = 236,
			CWeaponCSBase = 235,
			CC4 = 34,
			CBumpMine = 32,
			CBumpMineProjectile = 33,
			CBreachCharge = 28,
			CBreachChargeProjectile = 29,
			CWeaponBaseItem = 233,
			CBaseCSGrenade = 8,
			CSnowballProjectile = 160,
			CSnowballPile = 159,
			CSmokeGrenadeProjectile = 156,
			CSensorGrenadeProjectile = 152,
			CMolotovProjectile = 113,
			CItem_Healthshot = 104,
			CItemDogtags = 106,
			CDecoyProjectile = 48,
			CPhysPropRadarJammer = 126,
			CPhysPropWeaponUpgrade = 127,
			CPhysPropAmmoBox = 124,
			CPhysPropLootCrate = 125,
			CItemCash = 105,
			CEnvGasCanister = 63,
			CDronegun = 50,
			CParadropChopper = 115,
			CSurvivalSpawnChopper = 170,
			CBRC4Target = 27,
			CInfoMapRegion = 102,
			CFireCrackerBlast = 72,
			CInferno = 100,
			CChicken = 36,
			CDrone = 49,
			CFootstepControl = 79,
			CCSGameRulesProxy = 39,
			CWeaponCubemap = 0,
			CWeaponCycler = 237,
			CTEPlantBomb = 210,
			CTEFireBullets = 197,
			CTERadioIcon = 214,
			CPlantedC4 = 128,
			CCSTeam = 43,
			CCSPlayerResource = 41,
			CCSPlayer = 40,
			CPlayerPing = 130,
			CCSRagdoll = 42,
			CTEPlayerAnimEvent = 211,
			CHostage = 97,
			CHostageCarriableProp = 98,
			CBaseCSGrenadeProjectile = 9,
			CHandleTest = 95,
			CTeamplayRoundBasedRulesProxy = 173,
			CSpriteTrail = 165,
			CSpriteOriented = 164,
			CSprite = 163,
			CRagdollPropAttached = 147,
			CRagdollProp = 146,
			CPropCounter = 141,
			CPredictedViewModel = 139,
			CPoseController = 135,
			CGrassBurn = 94,
			CGameRulesProxy = 93,
			CInfoLadderDismount = 101,
			CFuncLadder = 85,
			CTEFoundryHelpers = 200,
			CEnvDetailController = 61,
			CDangerZone = 44,
			CDangerZoneController = 45,
			CWorldVguiText = 274,
			CWorld = 273,
			CWaterLODControl = 230,
			CWaterBullet = 229,
			CVoteController = 228,
			CVGuiScreen = 227,
			CPropJeep = 143,
			CPropVehicleChoreoGeneric = 0,
			CTriggerSoundOperator = 226,
			CBaseVPhysicsTrigger = 22,
			CTriggerPlayerMovement = 225,
			CBaseTrigger = 20,
			CTest_ProxyToggle_Networkable = 222,
			CTesla = 217,
			CBaseTeamObjectiveResource = 17,
			CTeam = 172,
			CSunlightShadowControl = 169,
			CSun = 168,
			CParticlePerformanceMonitor = 117,
			CSpotlightEnd = 162,
			CSpatialEntity = 161,
			CSlideshowDisplay = 154,
			CShadowControl = 153,
			CSceneEntity = 150,
			CRopeKeyframe = 148,
			CRagdollManager = 145,
			CPhysicsPropMultiplayer = 122,
			CPhysBoxMultiplayer = 120,
			CPropDoorRotating = 142,
			CBasePropDoor = 16,
			CDynamicProp = 52,
			CProp_Hallucination = 140,
			CPostProcessController = 136,
			CPointWorldText = 134,
			CPointCommentaryNode = 133,
			CPointCamera = 132,
			CPlayerResource = 131,
			CPlasma = 129,
			CPhysMagnet = 123,
			CPhysicsProp = 121,
			CStatueProp = 166,
			CPhysBox = 119,
			CParticleSystem = 118,
			CMovieDisplay = 114,
			CMaterialModifyControl = 110,
			CLightGlow = 109,
			CItemAssaultSuitUseable = 0,
			CItem = 0,
			CInfoOverlayAccessor = 103,
			CFuncTrackTrain = 92,
			CFuncSmokeVolume = 91,
			CFuncRotating = 90,
			CFuncReflectiveGlass = 89,
			CFuncOccluder = 88,
			CFuncMoveLinear = 87,
			CFuncMonitor = 86,
			CFunc_LOD = 81,
			CTEDust = 192,
			CFunc_Dust = 80,
			CFuncConveyor = 84,
			CFuncBrush = 83,
			CBreakableSurface = 31,
			CFuncAreaPortalWindow = 82,
			CFish = 75,
			CFireSmoke = 73,
			CEnvTonemapController = 69,
			CEnvScreenEffect = 67,
			CEnvScreenOverlay = 68,
			CEnvProjectedTexture = 65,
			CEnvParticleScript = 64,
			CFogController = 78,
			CEnvDOFController = 62,
			CCascadeLight = 35,
			CEnvAmbientLight = 60,
			CEntityParticleTrail = 59,
			CEntityFreezing = 58,
			CEntityFlame = 57,
			CEntityDissolve = 56,
			CDynamicLight = 51,
			CColorCorrectionVolume = 38,
			CColorCorrection = 37,
			CBreakableProp = 30,
			CBeamSpotlight = 25,
			CBaseButton = 5,
			CBaseToggle = 19,
			CBasePlayer = 15,
			CBaseFlex = 12,
			CBaseEntity = 11,
			CBaseDoor = 10,
			CBaseCombatCharacter = 6,
			CBaseAnimatingOverlay = 3,
			CBoneFollower = 26,
			CBaseAnimating = 2,
			CAI_BaseNPC = 0,
			CBeam = 24,
			CBaseViewModel = 21,
			CBaseParticleEntity = 14,
			CBaseGrenade = 13,
			CBaseCombatWeapon = 7,
			CBaseWeaponWorldModel = 23
		}
		public static string[] classArr = Enum.GetNames(typeof(ClassID));
		public static Array classArrVals = Enum.GetValues(typeof(ClassID));
	}
}
