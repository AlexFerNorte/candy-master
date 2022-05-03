using System;
using Obi;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Forming
{
    public class FormingHub : MonoBehaviour
    {
        [SerializeField] private ObiSolver obiSolver;

        private void Start()
        {
            print($"Forming Hub count {obiSolver.actors.Count}");
            foreach (var obiSolverActor in obiSolver.actors)
            {
                //obiSolverActor
                print(obiSolverActor.name);
            }
        }
    }
}