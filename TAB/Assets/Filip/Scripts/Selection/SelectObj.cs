using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

//Wybieranie obiektów oraz two¿enie do nich odpowiednich buttonów
public class SelectObj : MonoBehaviour
{
    [SerializeField] private HUDMenager hudMenager;
    [SerializeField] LayerMask ground;
    SelectDictionary selected_table;
    RaycastHit hit;
    public List<Button> buttonList = new List<Button>();

    bool dragSelect;



    MeshCollider selectionBox;
    Mesh selectionMesh;

    Vector3 p1;
    Vector3 p2;

    Vector2[] corners;

    Vector3[] verts;
    Vector3[] vecs;

    void Start()
    {

        selected_table = GetComponent<SelectDictionary>();
        dragSelect = false;
    }

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            p1 = Input.mousePosition;
        }


        if (Input.GetMouseButton(0))
        {
            if ((p1 - Input.mousePosition).magnitude > 40)
            {
                dragSelect = true;
            }
        }


        if (Input.GetMouseButtonUp(0))
        {
            if (dragSelect == false)
            {
                Ray ray = Camera.main.ScreenPointToRay(p1);

                if (Physics.Raycast(ray, out hit, 50000.0f, ground))
                {                  
                    if (Input.GetKey(KeyCode.LeftShift) && hit.transform.gameObject.tag == "Squad")
                    {
                        Debug.Log(hit.point);

                        selected_table.addSelected(hit.transform.gameObject);
                        AddButton();
                    }                    
                    else
                    {

                        selected_table.addSelected(hit.transform.gameObject);
                        AddButton();
                    }
                }
                else
                {
                    if (Input.GetKey(KeyCode.LeftShift))
                    {

                    }
                    else
                    {
                        selected_table.deselectAll();
                        DeleteButton();
                    }
                }
            }
            else
            {
                verts = new Vector3[4];
                vecs = new Vector3[4];
                int i = 0;
                p2 = Input.mousePosition;
                corners = getBoundingBox(p1, p2);

                foreach (Vector2 corner in corners)
                {
                    Ray ray = Camera.main.ScreenPointToRay(corner);

                    if (Physics.Raycast(ray, out hit, 50000.0f, (1 << 8)))
                    {
                        verts[i] = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                        vecs[i] = ray.origin - hit.point;
                        Debug.DrawLine(Camera.main.ScreenToWorldPoint(corner), hit.point, Color.red, 1.0f);
                    }
                    i++;
                }

                selectionMesh = generateSelectionMesh(verts, vecs);

                selectionBox = gameObject.AddComponent<MeshCollider>();
                selectionBox.sharedMesh = selectionMesh;
                selectionBox.convex = true;
                selectionBox.isTrigger = true;

                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    selected_table.deselectAll();
                    DeleteButton();
                }

                Destroy(selectionBox, 0.02f);

            }

            dragSelect = false;

        }

    }

    void AddButton()
    {
        DeleteButton();

        if (selected_table.selectedTable.Count > 0)
        {
            //if (selected_table.selectedTable.Count <= 5)
            //{
                hudMenager.OneRowButtons.SetActive(true);
                hudMenager.TwoRowButtons.SetActive(false);

                foreach (var item in selected_table.selectedTable.Values)
                {
                    var createdButton = Instantiate(item.GetComponent<SquadAndUniteButtonHendeler>().SquadButtonPrefab, hudMenager.OneRowButtons.transform);
                    createdButton.GetComponent<SquadAndUniteButtonHendeler>().SquadConnectedToButton = item;
                    buttonList.Add(createdButton);
                }
            //}
            //else
            //{
            //    hudMenager.TwoRowButtons.SetActive(true);
            //    hudMenager.OneRowButtons.SetActive(false);

            //    int i = 0;
            //    foreach (var item in selected_table.selectedTable.Values)
            //    {
            //        var row = i < 5 ? hudMenager.TwoRowButtons.GetComponent<TwoRowMenager>().FirstRow : hudMenager.TwoRowButtons.GetComponent<TwoRowMenager>().SecondRow;
            //        var createdButton = Instantiate(item.GetComponent<SquadAndUniteButtonHendeler>().SquadButtonPrefab, row.transform);
            //        createdButton.GetComponent<SquadAndUniteButtonHendeler>().SquadConnectedToButton = item;
            //        buttonList.Add(createdButton);
            //        i++;
            //    }
            //}
        }
        else
        {
            hudMenager.OneRowButtons.SetActive(false);
            hudMenager.TwoRowButtons.SetActive(false);
        }
    }



    void DeleteButton()
    {

        foreach (var item in buttonList)
        {
            Destroy(item.gameObject);
        }
        buttonList.Clear();
        hudMenager.OneRowButtons.SetActive(false);
        hudMenager.TwoRowButtons.SetActive(false);

    }
    private void OnGUI()
    {
        if (dragSelect == true)
        {
            var rect = Utils.GetScreenRect(p1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    Vector2[] getBoundingBox(Vector2 p1, Vector2 p2)
    {
        var bottomLeft = Vector3.Min(p1, p2);
        var topRight = Vector3.Max(p1, p2);

        Vector2[] corners =
        {
            new Vector2(bottomLeft.x, topRight.y),
            new Vector2(topRight.x, topRight.y),
            new Vector2(bottomLeft.x, bottomLeft.y),
            new Vector2(topRight.x, bottomLeft.y)
        };
        return corners;

    }

    Mesh generateSelectionMesh(Vector3[] corners, Vector3[] vecs)
    {
        Vector3[] verts = new Vector3[8];
        int[] tris = { 0, 1, 2, 2, 1, 3, 4, 6, 0, 0, 6, 2, 6, 7, 2, 2, 7, 3, 7, 5, 3, 3, 5, 1, 5, 0, 1, 1, 4, 0, 4, 5, 6, 6, 5, 7 }; //map the tris of our cube

        for (int i = 0; i < 4; i++)
        {
            verts[i] = corners[i];
        }

        for (int j = 4; j < 8; j++)
        {
            verts[j] = corners[j - 4] + vecs[j - 4];
        }

        Mesh selectionMesh = new Mesh();
        selectionMesh.vertices = verts;
        selectionMesh.triangles = tris;

        return selectionMesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Squad")
        {
            selected_table.addSelected(other.gameObject);
            AddButton();
        }
        else if (other.gameObject.tag == "Builder")
        {
            selected_table.addSelected(other.gameObject);
            AddButton();
        }
    }
}
