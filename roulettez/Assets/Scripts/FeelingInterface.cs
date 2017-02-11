using UnityEngine;
using System.Collections;

public interface FeelingInterface {
    string feeling { get; set; }
    FeelingType type { get; set; }
    int id { get; set; }

    int GetID();
    string GetFeeling();
    FeelingType GetType();
  
}
public enum FeelingType { Negative = -1, Positive = 1, Neutral = 0 };
