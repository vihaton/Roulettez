using UnityEngine;
using System.Collections;

public interface FeelingInterface {
    string feeling { get; set; }
    int type { get; set; }
    int id { get; set; }

    int GetID();
    string GetFeeling();
    int GetType();
}
