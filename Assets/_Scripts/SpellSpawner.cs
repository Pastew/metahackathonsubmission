using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class SpellSpawner : MonoBehaviour
{
   public void SpawnSpell(Spell spellToSpawn)
   {
      GameObject go = Instantiate(spellToSpawn).gameObject;
      go.transform.parent = this.transform;
      go.transform.localPosition = Vector3.zero;
   }
}
