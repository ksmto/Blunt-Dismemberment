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

namespace BluntDismemberment
{
	public class Entry : LevelModule
	{
		public static Configuration configuration = new Configuration();

		public override IEnumerator OnLoadCoroutine()
		{
			EventManager.onCreatureHit += EventManager_onCreatureHit;
			configuration = JsonConvert.DeserializeObject<Configuration>(File.ReadAllText(
				Path.Combine(Application.streamingAssetsPath, "Mods", "Blunt Dismemberment", "Configuration.json")));
			return base.OnLoadCoroutine();
		}

		private void EventManager_onCreatureHit(Creature creature, CollisionInstance collisionInstance)
		{
			if (collisionInstance.damageStruct.damageType == DamageType.Blunt)
			{
				if (configuration.bluntDismembermentEnabled)
				{
					if (configuration.headBluntDismembermentEnabled)
						if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.Head)
							if (collisionInstance.damageStruct.damage >= configuration.minimumDamageToDismember)
								creature.GetRagdollPart(RagdollPart.Type.Head).TrySlice();

					if (configuration.handBluntDismembermentEnabled)
					{
						if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftHand)
							if (collisionInstance.damageStruct.damage >= configuration.minimumDamageToDismember)
								creature.GetRagdollPart(RagdollPart.Type.LeftHand).TrySlice();

						if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightHand)
							if (collisionInstance.damageStruct.damage >= configuration.minimumDamageToDismember)
								creature.GetRagdollPart(RagdollPart.Type.RightHand).TrySlice();
					}

					if (configuration.armBluntDismembermentEnabled)
					{
						if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftArm)
							if (collisionInstance.damageStruct.damage >= configuration.minimumDamageToDismember)
								creature.GetRagdollPart(RagdollPart.Type.LeftArm).TrySlice();

						if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightArm)
							if (collisionInstance.damageStruct.damage >= configuration.minimumDamageToDismember)
								creature.GetRagdollPart(RagdollPart.Type.RightArm).TrySlice();
					}

					if (configuration.legBluntDismembermentEnabled)
					{
						if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftLeg)
							if (collisionInstance.damageStruct.damage >= configuration.minimumDamageToDismember)
								creature.GetRagdollPart(RagdollPart.Type.LeftLeg).TrySlice();

						if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightLeg)
							if (collisionInstance.damageStruct.damage >= configuration.minimumDamageToDismember)
								creature.GetRagdollPart(RagdollPart.Type.RightLeg).TrySlice();
					}

					if (configuration.footBluntDismembermentEnabled)
					{
						if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftFoot)
							if (collisionInstance.damageStruct.damage >= configuration.minimumDamageToDismember)
								creature.GetRagdollPart(RagdollPart.Type.LeftFoot).TrySlice();

						if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightFoot)
							if (collisionInstance.damageStruct.damage >= configuration.minimumDamageToDismember)
								creature.GetRagdollPart(RagdollPart.Type.RightFoot).TrySlice();
					}
				}

				if (configuration.punchDismembermentEnabled)
					if (collisionInstance.damageStruct.damager.data.id == "Punch")
					{
						if (configuration.headBluntDismembermentEnabled)
							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.Head)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.Head).TrySlice();

						if (configuration.handBluntDismembermentEnabled)
						{
							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftHand)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.LeftHand).TrySlice();

							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightHand)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.RightHand).TrySlice();
						}

						if (configuration.armBluntDismembermentEnabled)
						{
							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftArm)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.LeftArm).TrySlice();

							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightArm)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.RightArm).TrySlice();
						}

						if (configuration.legBluntDismembermentEnabled)
						{
							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftLeg)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.LeftLeg).TrySlice();

							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightLeg)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.RightLeg).TrySlice();
						}

						if (configuration.footBluntDismembermentEnabled)
						{
							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftFoot)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.LeftFoot).TrySlice();

							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightFoot)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.RightFoot).TrySlice();
						}
					}

				if (configuration.kickDismembermentEnabled)
					if (collisionInstance.damageStruct.damager.data.id == "Kick")
					{
						if (configuration.headBluntDismembermentEnabled)
							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.Head)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.Head).TrySlice();

						if (configuration.handBluntDismembermentEnabled)
						{
							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftHand)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.LeftHand).TrySlice();

							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightHand)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.RightHand).TrySlice();
						}

						if (configuration.armBluntDismembermentEnabled)
						{
							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftArm)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.LeftArm).TrySlice();

							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightArm)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.RightArm).TrySlice();
						}

						if (configuration.legBluntDismembermentEnabled)
						{
							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftLeg)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.LeftLeg).TrySlice();

							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightLeg)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.RightLeg).TrySlice();
						}

						if (configuration.footBluntDismembermentEnabled)
						{
							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.LeftFoot)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.LeftFoot).TrySlice();

							if (collisionInstance.damageStruct.hitRagdollPart.type == RagdollPart.Type.RightFoot)
								if (collisionInstance.damageStruct.damage >= 1.0f)
									creature.GetRagdollPart(RagdollPart.Type.RightFoot).TrySlice();
						}
					}
			}
		}
	}
}