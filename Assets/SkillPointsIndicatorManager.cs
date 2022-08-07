using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillPointsIndicatorManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> skillListIndicator;
  //  public List<Image> firstSkill;
   // public List<Image> secondSkill;
   // public List<Image> thirdSkill;
    //public List<Image> lastSkill;
    List<Image> skillImage = new List<Image>();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    OnUpgradeSkill();
        //}
    }

    public void OnUpgradeSkill(int skillLevel, int skillIndex)
    {

      //  GameObject image = skillListIndicator[skillIndex];


        Image[] allChildren = skillListIndicator[skillIndex].GetComponentsInChildren<Image>();

        foreach(Image images in allChildren)
        {
            skillImage.Add(images);

        }

    //    Debug.Log(skillImage);

        skillImage[skillLevel].color = Color.yellow;

        skillImage.Clear();

    }

    Image SkillListDecider(int index)
    {
        return skillListIndicator[index].GetComponent<Image>();
    }


    private void OnDisable()
    {
    }
}
