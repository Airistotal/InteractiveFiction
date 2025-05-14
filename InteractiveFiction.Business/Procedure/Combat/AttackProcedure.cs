using InteractiveFiction.Business.Entity;
using InteractiveFiction.Business.Goal;

namespace InteractiveFiction.Business.Procedure.Combat
{
    public class AttackProcedure : IProcedure
    {
        private readonly IEntity Entity;
        private readonly IAgent Agent;
        private readonly IDamager Damager;
        private IDamageable? DamageableTarget;

        private string? TargetName;

        public AttackProcedure(IAgent agent)
        {
            if (agent is IEntity entity && agent is IDamager damager)
            {
                Entity = entity;
                Agent = agent;
                Damager = damager;
            } else
            {
                throw new Exception("Unable to attack without proper interfaces.");
            }
        }

        public void Perform()
        {
            if (DamageableTarget == null)
            {
                Agent.AddEvent($"Unable to find {TargetName} to attack.");
                return;
            }

            var dmg = Damager.CalculateDamage();
            DamageableTarget.ReceiveDamage(Damager, dmg);

            LogEvent(dmg);
        }

        private void LogEvent(int dmg)
        {
            Agent.AddEvent($"You hit {TargetName} for {dmg} damage.");

            if (DamageableTarget is IAgent damagedAgent)
            {
                damagedAgent.AddEvent($"You were hit by {Entity.GetName()} for {dmg} damage.");
            }
        }

        public IProcedure With(List<IProcedureArg> args)
        {
            if (args != null && args.Count > 0 && args[0] is AttackArg atkArg)
            {
                TargetName = atkArg.TargetName;
                var target = Entity.GetLocation().GetTarget(atkArg.TargetName);

                if (target is IDamageable damageable)
                {
                    DamageableTarget = damageable;
                }
            }

            return this;
        }

        public IStat GetAsStat()
        {
            throw new NotImplementedException();
        }
    }
}
