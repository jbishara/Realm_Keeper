﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace Invector.vCharacterController
{
    public class vThirdPersonInput : MonoBehaviour
    {
        #region Variables

        private bool sprintToggle = false;

        [Header("Controller Input")]
        public string horizontalInput = "Horizontal";
        public string verticallInput = "Vertical";
        public KeyCode jumpInput = KeyCode.Space;
        public KeyCode strafeInput = KeyCode.Tab;
        public KeyCode sprintInput = KeyCode.LeftShift;

        [Header("Camera Input")]
        public string rotateCameraXInput = "Mouse X";
        public string rotateCameraYInput = "Mouse Y";
        [Header("Our custome Inputs")]
        public CharacterClass whatCharacter;
        public LD_AudioManager audioManager;


        [HideInInspector] public vThirdPersonController cc;
        [HideInInspector] public vThirdPersonCamera tpCamera;
        [HideInInspector] public Camera cameraMain;

        #endregion

        private void Awake()
        {
            //SceneManager.sceneLoaded += InitialiseThis;
            SceneManager.activeSceneChanged += InitialiseThis;

        }

        private void InitialiseThis(Scene current, Scene next)
        {
            InitializeTpCamera();
        }

        protected virtual void Start()
        {
            InitilizeController();
            InitializeTpCamera();
            //audioManager = GameObject.Find("AudioManager").GetComponent<LD_AudioManager>();
            audioManager = Master_Script.instance.audioManager.GetComponent<LD_AudioManager>();
        }

        protected virtual void FixedUpdate()
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
        }

        protected virtual void Update()
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
        }

        public virtual void OnAnimatorMove()
        {
            cc.ControlAnimatorRootMotion(); // handle root motion animations 
        }

        #region Basic Locomotion Inputs

        protected virtual void InitilizeController()
        {
            cc = GetComponent<vThirdPersonController>();

            if (cc != null)
                cc.Init();
        }

        protected virtual void InitializeTpCamera()
        {
            if (tpCamera == null)
            {
                tpCamera = FindObjectOfType<vThirdPersonCamera>();
                if (tpCamera == null)
                    return;
                if (tpCamera)
                {
                    tpCamera.SetMainTarget(this.transform);
                    tpCamera.Init();
                }
            }
        }

        protected virtual void InputHandle()
        {
            MoveInput();
            CameraInput();
            SprintInput();
            StrafeInput();
            JumpInput();
        }

        public virtual void MoveInput()
        {

            // put in Zylar walk audiofile
            // if(whatCharacter == ("Zylar"))
            cc.input.x = Input.GetAxis(horizontalInput);
            cc.input.z = Input.GetAxis(verticallInput);
        }

        protected virtual void CameraInput()
        {
            if (!cameraMain)
            {
                if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
                else
                {
                    cameraMain = Camera.main;
                    cc.rotateTarget = cameraMain.transform;
                }
            }

            if (cameraMain)
            {
                cc.UpdateMoveDirection(cameraMain.transform);
            }

            if (tpCamera == null)
                return;

            var Y = Input.GetAxis(rotateCameraYInput);
            var X = Input.GetAxis(rotateCameraXInput);

            tpCamera.RotateCamera(X, Y);
        }

        protected virtual void StrafeInput()
        {
            if (Input.GetKeyDown(strafeInput))
                cc.Strafe();
        }

        protected virtual void SprintInput()
        {
            if(Input.GetKeyDown(sprintInput))
            {
                sprintToggle = !sprintToggle;
                // put in Zylar sprint audiofile
                // if(whatCharacter == ("Zylar"))
                cc.Sprint(sprintToggle);
            }

            //if (Input.GetKeyDown(sprintInput))
            //    cc.Sprint(true);
            //else if (Input.GetKeyUp(sprintInput))
            //    cc.Sprint(false);
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
            {
                cc.Jump();
                if(whatCharacter == CharacterClass.Zylar)
                {
                    // does a random role between 0 2
                    int min = 0;
                    int max = 3;
                    int result = Random.Range(min, max);
                    // based on the result playes a random jump sound
                    switch (result)
                    {
                        case 0:
                            audioManager.Play("Zylar_Jump_V01");
                            Debug.Log("Audio v1");
                            break;
                        case 1:
                            audioManager.Play("Zylar_Jump_V02");
                            Debug.Log("Audio v2");
                            break;
                        case 2:
                            audioManager.Play("Zylar_Jump_V03");
                            Debug.Log("Audio v3");
                            break;
                    }
                }
            }
        }

        #endregion       
    }
}