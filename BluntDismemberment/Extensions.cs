using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
using System.Text;
using System.Speech;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Reflection;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using Newtonsoft;
using Newtonsoft.Json;
using HarmonyLib;
using ThunderRoad;
using Extensions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using UnityEngine.VFX;
using UnityEngine.Rendering;
using Action = System.Action;
using Object = System.Object;
using Random = UnityEngine.Random;
using Methods = Extensions.Methods;

namespace Extensions
{
	internal static class Methods
	{
		public static RagdollPart GetRagdollPart(this Creature creature, RagdollPart.Type ragdollPartType)
		{
			return creature.ragdoll.GetPart(ragdollPartType);
		}

		public static RagdollPart GetHeadPart(this Creature creature)
		{
			return creature.ragdoll.headPart;
		}

		public static Creature NearestCreature()
		{
			return Creature.allActive.Where(creature => creature.isKilled == false && creature.isPlayer == false)
				.OrderBy(creature => Vector3.Distance(Player.currentCreature.ragdoll.headPart.transform.position,
					creature.ragdoll.headPart.transform.position)).FirstOrDefault();
		}

		public static float DistanceBetweenCreatureAndPlayer(this Creature creature)
		{
			return Vector3.Distance(creature.transform.position, Player.currentCreature.player.transform.position);
		}

		public static PlayerControl.Hand ControlHand(this RagdollHand hand)
		{
			return hand.playerHand.controlHand;
		}

		public static bool EmptyHand(this RagdollHand hand)
		{
			return hand.grabbedHandle == null &&
				   hand.caster.telekinesis.catchedHandle == null &&
				   hand.caster.isFiring == false &&
				   hand.caster.isMerging == false;
		}

		// Controls
		public static bool GripPressed(this RagdollHand hand)
		{
			return ControlHand(hand).gripPressed;
		}

		public static bool TriggerPressed(this RagdollHand hand)
		{
			return ControlHand(hand).usePressed;
		}

		public static bool AlternateUsePressed(this RagdollHand hand)
		{
			return ControlHand(hand).alternateUsePressed;
		}

		public static float DistanceBetweenHands()
		{
			return Vector3.Distance(GetHandSide(Side.Left).transform.position,
				GetHandSide(Side.Right).transform.position);
		}

		public static RagdollHand GetHandSide(this Side side)
		{
			return Player.currentCreature.GetHand(side);
		}

		public static Vector3 PalmDirection(this RagdollHand hand)
		{
			return hand.PalmDir;
		}

		// Palmar & Dorsal hand pos and rotations
		public static Vector3 DorsalHandPosition(this RagdollHand hand)
		{
			return -hand.palmCollider.transform.forward * -0.2f + hand.palmCollider.transform.position;
		}

		public static Quaternion DorsalHandRotation(this RagdollHand hand)
		{
			return hand.palmCollider.transform.rotation;
		}

		public static Vector3 PalmarHandPosition(this SpellCaster spellCaster)
		{
			return spellCaster.magic.position;
		}

		public static Quaternion PalmarHandRotation(this SpellCaster spellCaster)
		{
			return spellCaster.magic.rotation;
		}

		// Hand velocities
		public static Vector3 HandVelocity(this RagdollHand hand)
		{
			return Player.local.transform.rotation * hand.playerHand.controlHand.GetHandVelocity();
		}

		public static float GetHandVelocityDirection(this RagdollHand hand, Vector3 direction)
		{
			return Vector3.Dot(hand.HandVelocity(), direction);
		}

		// Transforms and stuff
		public static Transform ThumbFingerTip(this RagdollHand hand)
		{
			return hand.fingerThumb.tip.transform;
		}

		public static Transform IndexFingerTip(this RagdollHand hand)
		{
			return hand.fingerIndex.tip.transform;
		}

		public static Transform MiddleFingerTip(this RagdollHand hand)
		{
			return hand.fingerMiddle.tip.transform;
		}

		public static Transform RingFingerTip(this RagdollHand hand)
		{
			return hand.fingerRing.tip.transform;
		}

		public static Transform PinkyFingerTip(this RagdollHand hand)
		{
			return hand.fingerLittle.tip.transform;
		}

		public static Transform HandPalm(this RagdollHand hand)
		{
			return hand.palmCollider.transform;
		}

		public static Transform HandTransform(this RagdollHand hand)
		{
			return hand.transform;
		}

		public static Vector3 HandPosition(this RagdollHand hand)
		{
			return hand.transform.position;
		}

		public static Quaternion HandRotation(this RagdollHand hand)
		{
			return hand.transform.rotation;
		}

		public static Transform UpperArmPart(this RagdollHand hand)
		{
			return hand.upperArmPart.transform;
		}

		public static Transform LowerArmPart(this RagdollHand hand)
		{
			return hand.lowerArmPart.transform;
		}

		public static void SnapRagdollPart(this Creature creature, RagdollPart.Type ragdollPartType)
		{
			creature.ragdoll.GetPart(ragdollPartType).DisableCharJointLimit();
			creature.ragdoll.GetPart(ragdollPartType).rb.useGravity = true;
		}

		public static void FreezeCreature(this Creature creature, float colorAmount)
		{
			creature.ragdoll.SetState(Ragdoll.State.Frozen);
			creature.SetColor(Color.Lerp(Color.blue, creature.GetColor(Creature.ColorModifier.Skin), colorAmount), Creature.ColorModifier.Skin, true);
		}

		public static void FreezeItem(this Item item)
		{
			item.rb.isKinematic = true;
			item.rb.useGravity = false;
		}

		public static void UnfreezeItem(this Item item)
		{
			item.rb.isKinematic = false;
			item.rb.useGravity = true;
		}
	}
}