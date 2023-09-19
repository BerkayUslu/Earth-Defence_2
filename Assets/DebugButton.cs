using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugButton : MonoBehaviour
{
    private ISkill _skillManager;
    public List<string> _skillNames;
    [SerializeField] GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        _skillManager = GameObject.Find("Game Manager").GetComponent<ISkill>();
        _skillNames = _skillManager.GetSkillNames();

    }

    public void LevelSkillUp()
    {
        _skillManager.SkillLevelUp("Arrow Rain");
        canvas.SetActive(false);
    }


}
