using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.AI;

public class RangeBase : Icondition
{
  
  protected  ChaseState _chaseState;

  protected AttackState _attackState;
  protected NavMeshAgent en;
  protected GameObject pl;
  protected float range; 
  protected float attackRange;
  
   public RangeBase(ChaseState cs, NavMeshAgent enemy, GameObject player, float range )
   {
        _chaseState = cs;
        en = enemy;
        pl = player;
        this.range = range;
       

   }
   public RangeBase( AttackState ats, NavMeshAgent enemy, GameObject player, float range )
   {
        _attackState = ats;
        en = enemy;
        pl = player;
        attackRange = range;
       

   }

   public  virtual bool Evaluate()
   {
     return Vector3.Distance(pl.transform.position, en.transform.position) <= range && !_chaseState.chase;
    
   }
}


public class BackToPatrol : RangeBase
{
 
  public BackToPatrol(ChaseState cs, NavMeshAgent enemy, GameObject player, float range ) : base(cs, enemy, player, range)
   {    

   }


  public override bool Evaluate()
   {
     return Vector3.Distance(base.pl.transform.position, base.en.transform.position) > range && base._chaseState.chase;
    
   }
}


public class AttackRange : RangeBase
{
  
  public AttackRange(AttackState ats, NavMeshAgent enemy, GameObject player, float range ) : base( ats,enemy, player, range)
   {    

   }


  public override bool Evaluate()
   {
     return Vector3.Distance(base.pl.transform.position, base.en.transform.position) <= base.attackRange && !base._attackState.attack;
    
   }
}
