using UnityEngine;
using Polarith.AI.Move;
using PAI = Polarith.AI.Package;

namespace Invector.vCharacterController
{
    public class ControllerNPC : MonoBehaviour
    {
        #region Variables       

        // AI movement variables
        [Tooltip("This component provides the results of the AI system. These results are then applied to the " +
            "attached Animator. Thus, the reference to an AIMContext component is mandatory.The controller is + " +
            "disabled if no Context instance can be found at OnEnable.")]
        public AIMContext Context;
        public float RotationSpeed = 1.0f;
        public float SpeedMultiplyer = 0.5f;
        [TargetObjective(true)]
        public int ObjectiveAsSpeed = 0;


        [Header("Controller Input")]
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;

        [HideInInspector] public vThirdPersonController cc;

        #endregion

        #region Initiallization
        protected virtual void Start()
        {
            InitilizeController();
            InitilizePAI();
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            BehaviourHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }
        protected virtual void InitilizePAI()
        {
            if (Context == null)
                Context = GetComponent<AIMContext>();

            if (Context == null)
            {
                Debug.LogWarning("reference to AIMContext is missing.");
                enabled = false;
                return;
            }
        }

        #endregion

        #region Basic Locomotion

        protected virtual void BehaviourHandle()
        {
            MoveCharacter();
            RotateToTarget();
            // SprintInput();
            // StrafeInput();
            // JumpInput();
        }

        public virtual void MoveCharacter()
        {
            // Move the character
            if (ObjectiveAsSpeed >= 0 && ObjectiveAsSpeed < Context.DecidedValues.Count)
            {
                cc.input.z = Context.DecidedValues[ObjectiveAsSpeed] * SpeedMultiplyer;
            }
            else
            {
                cc.input.z = 1.0f * SpeedMultiplyer;
            }
        }

        protected virtual void RotateToTarget()
        {
            // Rotate the character
            Vector3 targetDirection = Context.DecidedDirection;
            float step = RotationSpeed * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, step, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            cc.UpdateMoveDirection(transform);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if (Input.GetKeyDown(sprintInput))
                cc.Sprint(true);
            else if (Input.GetKeyUp(sprintInput))
                cc.Sprint(false);
        }

        /// <summary>
        /// Conditions to trigger the Jump animation & behavior
        /// </summary>
        /// <returns></returns>
        protected virtual bool JumpConditions()
        {
            return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
        }

        /// <summary>
        /// Input to trigger the Jump 
        /// </summary>
        protected virtual void JumpInput()
        {
            if (Input.GetKeyDown(jumpInput) && JumpConditions())
                cc.Jump();
        }

        #endregion       
    }
}