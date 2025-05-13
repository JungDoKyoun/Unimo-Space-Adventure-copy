using UnityEngine;

namespace ZL.Unity.Phys
{
    [AddComponentMenu("ZL/Phys/Gravity Controller")]

    [DisallowMultipleComponent]

    [RequireComponent(typeof(Rigidbody))]

    public sealed class GravityController : MonoBehaviour
    {
        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [GetComponent]

        [ReadOnly(true)]

        #pragma warning disable CS0108

        private Rigidbody rigidbody;

        public Rigidbody Rigidbody
        {
            get => rigidbody;
        }

        #pragma warning restore CS0108

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Line(Margin = 0)]

        [Text("<b>Gravity</b>", FontSize = 16)]

        [Margin]

        private bool useGravity = true;

        [Space]

        [SerializeField]

        private bool useCustomGravity = false;

        public bool UseCustomGravity
        {
            get => useCustomGravity;

            set => useCustomGravity = value;
        }

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(useCustomGravity), true)]

        [AddIndent(1)]

        private GravityGenerator gravityGenerator;

        public GravityGenerator GravityGenerator
        {
            get => gravityGenerator;

            set => gravityGenerator = value;
        }

        [SerializeField]

        [UsingCustomProperty]

        [AddIndent(1)]

        [ToggleIf(nameof(useCustomGravity), false)]

        [Alias("Gravity Direction")]

        private Vector3 customGravityDirection = new Vector3(0f, -1f, 0f);

        public Vector3 CustomGravityDirection
        {
            get => customGravityDirection;
            
            set => customGravityDirection = value;
        }

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(useCustomGravity), false)]

        [AddIndent(1)]

        [Alias("Gravity Strength")]

        private float customGravityStrength = 9.81f;

        public float CustomGravityStrength
        {
            get => customGravityStrength;

            set => customGravityStrength = value;
        }

        [SerializeField]

        [UsingCustomProperty]

        [AddIndent(1)]

        private float gravityScale = 1f;

        public float GravityScale
        {
            get => gravityScale;

            set => gravityScale = value;
        }

        [Space]

        [SerializeField]

        private bool rotateVelocityUpright = true;

        private Vector3 gravityDirection = Vector3.zero;

        public Vector3 GravityDirection
        {
            get => gravityDirection;
        }

        private Vector3 oldGravityDirection;

        private Vector3 gravityForce = Vector3.zero;

        #if UNITY_EDITOR

        [Space]

        [SerializeField]

        [UsingCustomProperty]

        [Line(Margin = 0)]

        [Text("<b>Debugging</b>", FontSize = 16)]

        [Margin]

        [Alias("Draw Gravity Direction")]

        private bool drawGravityDirectionGizmo = true;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(drawGravityDirectionGizmo), false)]

        [AddIndent(1)]

        [Alias("Length")]

        private float gravityDirectionGizmoLength = 1000f;

        [SerializeField]

        [UsingCustomProperty]

        [ToggleIf(nameof(drawGravityDirectionGizmo), false)]

        [AddIndent(1)]

        [Alias("Color")]

        private Color gravityDirectionGizmoColor = Color.green;

        private void OnDrawGizmosSelected()
        {
            if (drawGravityDirectionGizmo == true)
            {
                Vector3 gravityDirection;

                if (useCustomGravity == true)
                {
                    gravityDirection = customGravityDirection;
                }

                else if (gravityGenerator != null)
                {
                    gravityDirection = gravityGenerator.transform.position - transform.position;
                }

                else
                {
                    gravityDirection = Physics.gravity;
                }

                gravityDirection = gravityDirection.normalized;

                Gizmos.color = gravityDirectionGizmoColor;

                Gizmos.DrawLine(transform.position, transform.position + gravityDirection * gravityDirectionGizmoLength);
            }
        }

        #endif

        private void FixedUpdate()
        {
            oldGravityDirection = gravityDirection;

            if (useGravity == false || gravityScale == 0f)
            {
                gravityDirection = Vector3.zero;

                gravityForce = Vector3.zero;
            }

            else
            {
                var gravityScale = this.gravityScale;

                if (useCustomGravity == true)
                {
                    gravityDirection = customGravityDirection.normalized;

                    gravityForce = customGravityStrength * gravityScale * gravityDirection;
                }

                else if (gravityGenerator != null)
                {
                    gravityDirection = gravityGenerator.GetGravityDirection(transform.position).normalized;

                    gravityForce = gravityGenerator.GravityStrength * gravityScale * gravityDirection;
                }

                else
                {
                    gravityDirection = Physics.gravity.normalized;

                    gravityForce = Physics.gravity * gravityScale;
                }
            }

            if (gravityDirection != Vector3.zero)
            {
                if (rotateVelocityUpright == true)
                {
                    rigidbody.velocity = Quaternion.FromToRotation(oldGravityDirection, gravityDirection) * rigidbody.velocity;
                }

                rigidbody.AddForce(gravityForce, ForceMode.Acceleration);
            }
        }
    }
}