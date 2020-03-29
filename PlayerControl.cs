using RPG.CameraUI;
using UnityEngine;
using System.Collections;
using System;

namespace RPG.Characters
{
    public class PlayerControl : MonoBehaviour
    {
        WeaponSystem weaponSystem;
        Character character;
        SpecialAbilities abilities;

        bool shiftPressed = false;
              
        void Start()
        {
            weaponSystem = GetComponent<WeaponSystem>();
            character = GetComponent<Character>();
            abilities = GetComponent<SpecialAbilities>();
            RegisterForMouseEvents();          
        }

        private void RegisterForMouseEvents()
        {
            CameraRaycaster cameraRaycaster = FindObjectOfType<CameraRaycaster>();
            cameraRaycaster.onMouseOverEnemy += OnMouseOverEnemy;
            cameraRaycaster.onMouseOverPotentiallyWalkable += OnMouseOverPotentiallyWalkable;
            cameraRaycaster.onMouseOverPickable += OnMouseOverPickable;
        }

        private void Update()
        {
            IsShiftPressed();
            //LookTowardsCursor();
            ScanForDefensiveMovementKeyUp();
            ScanForAbilityKeyDown();
        }

        void IsShiftPressed()
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                shiftPressed = true;
            }
            else
            {
                shiftPressed = false;
            }
        }

        //private void LookTowardsCursor()
        //{
            //1st method:
            //float mouseInput = Input.GetAxis("Mouse X");
            //Vector3 lookhere = new Vector3(0, mouseInput, 0);
            //transform.Rotate(lookhere);
            //2nd method:
            //float h = 2f * Input.GetAxis("Mouse X");
            //float v = 2f * Input.GetAxis("Mouse Y");
            //transform.Rotate(v, h, 0);
        //}

        private void ScanForDefensiveMovementKeyUp()
        {
            if (Input.GetKey("w") && !shiftPressed && !character.IsDefending && !character.IsAttacking && !character.IsResting)
            {
                    float directionV = 1f;
                    float directionH = 0f;
                    character.Dodge(directionV, directionH);
            }

            if (Input.GetKeyUp("w") && shiftPressed && !character.IsDefending && !character.IsAttacking && !character.IsResting)
            {
                if (abilities.GetCurrentStamina() >= abilities.getRollingCost)
                {
                    abilities.ConsumeStamina(abilities.getRollingCost);
                    float directionV = 2f;
                    float directionH = 0f;
                    character.Dodge(directionV, directionH);
                }
                else
                {
                    float directionV = 1f;
                    float directionH = 0f;
                    character.Dodge(directionV, directionH);
                }
            }

            if (Input.GetKey("s") && !shiftPressed && !character.IsDefending && !character.IsAttacking && !character.IsResting)
            {
                float directionV = -1f;
                float directionH = 0f;
                character.Dodge(directionV, directionH);
            }

            if (Input.GetKeyUp("s") && shiftPressed && !character.IsDefending && !character.IsAttacking && !character.IsResting)
            {
                if (abilities.GetCurrentStamina() >= abilities.getRollingCost)
                {
                    abilities.ConsumeStamina(abilities.getRollingCost);
                    float directionV = -2f;
                    float directionH = 0f;
                    character.Dodge(directionV, directionH);
                }
                else
                {
                    float directionV = -1f;
                    float directionH = 0f;
                    character.Dodge(directionV, directionH);
                }
            }

            if (Input.GetKey("a") && !shiftPressed && !character.IsDefending && !character.IsAttacking && !character.IsResting)
            {
                float directionV = 0f;
                float directionH = -1f;
                character.Dodge(directionV, directionH);
            }

            if (Input.GetKeyUp("a") && shiftPressed && !character.IsDefending && !character.IsAttacking && !character.IsResting)
            {
                if (abilities.GetCurrentStamina() >= abilities.getDodgeCost)
                {
                    abilities.ConsumeStamina(abilities.getDodgeCost);
                    float directionV = 0f;
                    float directionH = -2f;
                    character.Dodge(directionV, directionH);
                }
                else
                {
                    float directionV = 0f;
                    float directionH = -1f;
                    character.Dodge(directionV, directionH);
                }
            }

            if (Input.GetKey("d") && !shiftPressed && !character.IsDefending && !character.IsAttacking && !character.IsResting)
            {
                float directionV = 0f;
                float directionH = 1f;
                character.Dodge(directionV, directionH);
            }

            if (Input.GetKeyUp("d") && shiftPressed && !character.IsDefending && !character.IsAttacking && !character.IsResting)
            {
                if (abilities.GetCurrentStamina() >= abilities.getDodgeCost)
                {
                    abilities.ConsumeStamina(abilities.getDodgeCost);
                    float directionV = 0f;
                    float directionH = 2f;
                    character.Dodge(directionV, directionH);
                }
                else
                {
                    float directionV = 0f;
                    float directionH = 1f;
                    character.Dodge(directionV, directionH);
                }
            }

            if (Input.GetKeyUp("x") && !shiftPressed && !character.IsDefending && !character.IsAttacking)
            {
                character.Rest();
            }

            if (Input.GetMouseButtonUp(0) && shiftPressed && !character.IsDefending && !character.IsAttacking && !character.IsResting)
            {
                    weaponSystem.AttackOnce();
            }

            if (Input.GetMouseButtonUp(1) && shiftPressed && !character.IsDefending && !character.IsAttacking && !character.IsResting)
            {
                weaponSystem.Combo();
            }
        }        

        private void ScanForAbilityKeyDown()
        {
            for (int keyIndex = 1; keyIndex < abilities.GetNumberOfAbilities(); keyIndex++)
            {
                if (Input.GetKeyDown(keyIndex.ToString()))
                {
                    abilities.AttemptSpecialAbility(keyIndex);
                }
            }
        }

        void OnMouseOverPotentiallyWalkable (Vector3 destination) // reads LMB from CameraRaycaster if walkable layer hit
        {
            if (Input.GetMouseButton(0) && !shiftPressed && !character.IsDefending && !character.IsAttacking && !character.IsResting)
            {
                weaponSystem.StopAttacking(); // if target is fighting, stop the fight before moving
                character.SetDestination(destination);
            } else if (Input.GetMouseButton(0) && shiftPressed && !character.IsDefending && !character.IsAttacking)
            {
                var currentTransform = Vector3.Lerp(transform.position, destination, 2f);
                transform.LookAt(currentTransform);
            }
        }

        bool IsTargetInRange(GameObject target)
        {
            float distanceToTarget = (target.transform.position - transform.position).magnitude;
            return distanceToTarget <= weaponSystem.GetCurrentWeapon().GetMaxAttackRange();
        }

        void OnMouseOverEnemy(EnemyAI enemy) // reads LMB from CameraRaycaster if enemy layer hit
        {
            if (Input.GetMouseButton(0) && IsTargetInRange(enemy.gameObject))
            {
                transform.LookAt(enemy.gameObject.transform);
                weaponSystem.AttackOnce();
            }
            else if (Input.GetMouseButton(0) && !IsTargetInRange(enemy.gameObject))
            {
                StartCoroutine(MoveAndAttackOnce(enemy));
            }
            else if (Input.GetMouseButtonDown(1))
            {
                transform.LookAt(enemy.gameObject.transform);                
            }
            else if (Input.GetMouseButtonDown(1) && !IsTargetInRange(enemy.gameObject))
            {
                StartCoroutine(MoveToTarget(enemy.gameObject));
            }
        }

        IEnumerator MoveToTarget (GameObject target)
        {
            character.SetDestination(target.transform.position);
            while (!IsTargetInRange(target.gameObject))
            {
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForEndOfFrame();
        }

        IEnumerator MoveAndAttackRepeatedly (EnemyAI enemy)
        {
            yield return StartCoroutine(MoveToTarget(enemy.gameObject));
            weaponSystem.AttackTargetRepeatedly(enemy.gameObject);
        }

        IEnumerator MoveAndAttackOnce(EnemyAI enemy)
        {
            yield return StartCoroutine(MoveToTarget(enemy.gameObject));
            weaponSystem.AttackOnce();
        }

        IEnumerator MoveAndPowerAttack(EnemyAI enemy)
        {
            yield return StartCoroutine(MoveToTarget(enemy.gameObject));
            abilities.AttemptSpecialAbility(0, enemy.gameObject);
        }

        /* IMPORTANT: CameraRaycaster hits both WeaponPickupPoint, as well as its parented weapon prefab model. 
         * Data collected here is from the prefab, so need to use GetComponentInParent to get WeaponConfig needed by PutWeaponInHand method*/
        void OnMouseOverPickable (GameObject pickable) // reads LMB from CameraRaycaster if pickable layer hit
        {
            if (Input.GetMouseButtonUp(0))// TODO no pickup range - implement
            {
                transform.LookAt(pickable.gameObject.transform);
                WeaponConfig weaponToPick = pickable.GetComponentInParent<WeaponPickupPoint>().GetWeaponConfig(); // TODO split logic for different pickable types
                weaponSystem.PutWeaponInHand(weaponToPick);
                Destroy(pickable.gameObject); // TODO implement dropping used weapons
            }
            else if (Input.GetMouseButtonDown(1))// TODO relies on weapon range - change this
            {
                transform.LookAt(pickable.gameObject.transform);
            }
        }
    }
}

