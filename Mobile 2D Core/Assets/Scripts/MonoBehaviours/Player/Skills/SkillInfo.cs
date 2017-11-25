using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using Assets.Scripts.PlayerSkills;

public class SkillInfo : MonoBehaviour 
{
    public int id;
    public string Nombre;
    public string Descripcion;
    public Image img;
    public Text txtNombre;
    public Text txtDescripcion;
    public Text txtIdSeleccionado;

    void Start() 
    {
        var _skill = SkillLevels.Skills.Where(x => x.Name == Nombre).FirstOrDefault();
        if (_skill != null)
            Descripcion = _skill.Description;
    }

    public void Actualizar() 
    {
        txtNombre.text = Nombre;
        txtDescripcion.text = Descripcion;
        txtIdSeleccionado.text = id.ToString();
        img.sprite = GetComponent<Image>().sprite;
    }
}
