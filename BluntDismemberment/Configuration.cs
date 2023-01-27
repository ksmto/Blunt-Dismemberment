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
	public class Configuration
	{
		public bool bluntDismembermentEnabled = true;
		public bool punchDismembermentEnabled = false;
		public bool kickDismembermentEnabled = false;

		public bool headBluntDismembermentEnabled = true;
		public bool handBluntDismembermentEnabled = true;
		public bool armBluntDismembermentEnabled = true;
		public bool legBluntDismembermentEnabled = true;
		public bool footBluntDismembermentEnabled = true;

		public float minimumDamageToDismember = 12.0f;
	}
}
