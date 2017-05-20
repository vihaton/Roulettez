using UnityEngine;
using System.Collections.Generic;
using System;

public class TunneCreationScript : MonoBehaviour {


    public TextAsset accusationAsset;
    public TunneStructContainer TSC = new TunneStructContainer();
    private List<FeelingInterface> listOfFeelings;
    private List<List<string>> lines;
    [SerializeField]
    private int howManyFeelings;

    void Awake()
    {
        howManyFeelings = 0;
        string accusationData = accusationAsset.text;

        lines = new List<List<string>>();
        listOfFeelings = new List<FeelingInterface>();
        ReadLines(accusationData);
        GenerateFeelings();
        //SaveData();
        if (LoadData())
        {
            GenerateFeelingsFromXML();
        }
    }

    /*
	private string readData(string path) {
		string data = null;

		try {
			data = System.IO.File.ReadAllText(path);
		} catch {
			Debug.Log("ERROR : fucking problems @ AccusationControllerScript");
		}

		return data;
	}
    */
    public void SaveData()
    {
        try
        {
            TSC = TunneStructContainer.Load(Application.persistentDataPath + "/feelings.xml");
            
        }
        catch (Exception e)
        {
            TSC.Save(Application.persistentDataPath + "/feelings.xml",listOfFeelings);
        }
    }

    private bool LoadData()
    {
        try
        {
            TSC = TunneStructContainer.Load(Application.persistentDataPath + "/feelings.xml");
            return true;

        }
        catch (Exception e)
        {
            return false;
        }
    }

    private void ReadLines(string data)
    {
        Debug.Log("readLines");
        string[] rawLines = data.Split("\n"[0]);
        List<string> rawlist = new List<string>();
        rawlist.AddRange(rawLines);
        int max = rawLines.Length;

        for (int i = 0; i < max; i++)
        {
            lines.Add(chopLineSimple(rawLines[i]));
        }
    }


    private List<string> chopLineSimple(string wholeLine)
    {

        List<string> readyLine = new List<string>();
        readyLine.AddRange(wholeLine.Split(";"[0]));

        Debug.Log("Chopped Line: " + readyLine[0]);

        return readyLine;
    }
    /**
     * returns a list of accusationtexts of that line, size of how many type of survivals there are
     */
    private List<string> chopLine(string wholeLine)
    {
        Debug.Log("Chopchop: " + wholeLine);
        string[] line = wholeLine.Split(";"[0]);
        List<string> readyLine = new List<string>();
        int ind = 0;
        //Debug.Log("chopLine:\n" + wholeLine + "\n"  + "line length " +line.Length + " last piece: " +line[line.Length - 1]);

        string lastPiece = line[0];
        for (int i = 1; i < line.Length; i++)
        {
            bool choppedCell = false;
            string piece = line[i];

            if (lastPiece.StartsWith("\""))
            {
                piece = lastPiece + ";" + piece;
                choppedCell = true;
            }
            else if (piece.StartsWith("\""))
            {
                choppedCell = true;
            }

            //Debug.Log("piece: " + piece + "\nEndsWith(\"): " + piece.EndsWith("\"")); //tällä huomaa C# bugin: EndsWith palauttaa valheellisesti false, jos " on rivin lopussa
            if (piece.EndsWith("\"") || piece.IndexOf("\"") != piece.LastIndexOf("\""))
            { //solu on valmis
                int lastIndex = piece.Length - 2;
                if (!piece.EndsWith("\"")) lastIndex--; //sana loppui rivinvaihtoon

                piece = piece.Substring(1, lastIndex); //poistetaan " alusta ja lopusta
                readyLine.Insert(ind, piece);
                ind++;
                piece = "";

            }
            else if (!choppedCell)
            {
                readyLine.Insert(ind, piece);
                ind++;
            }

            lastPiece = piece;
        }
        //Debug.Log("line size " + readyLine.Count + " lastpiece: " + lastPiece);

        for (int i = 0; i < readyLine.Count; i++)
        {
            //Debug.Log("readyLine[i]" + readyLine[i] + Time.realtimeSinceStartup);
        }

        Debug.Log("Chopped Line: " + readyLine);
        return readyLine;
    }


    private void GenerateFeelings()
    {
        for (int i = 0; i < lines.Count; i++)
        {
           
            
                FeelingInterface FI;
                FI = new TunneStruct();
                FI.feeling = lines[i][0];
                if (FI.feeling.Length < 2)
                {
                    continue;
                }
                int type = Int32.Parse(lines[i][1]);
                FI.type = (FeelingType)type;
                FI.id = (i + 1) * 1000;
                listOfFeelings.Add(FI);
                howManyFeelings++;
                //Debug.Log(FI.feeling);
            }
        }

    private void GenerateFeelingsFromXML()
    {
        listOfFeelings.AddRange(TSC.TunneStructArray);
        howManyFeelings += TSC.TunneStructArray.GetLength(0);
    }


    /*private void GenerateFeelings()
    {
        for (int i = 0; i < lines.Count; i++)
        { //jokaiselle kolmesta rivistä
            int tunteenTyyppi = i-1;
            List<string> tunnelista = lines[i];

            for (int j = 0; j < tunnelista.Count; j++)
            {
                FeelingInterface FI;
                FI = new TunneStruct();
                FI.feeling = tunnelista[j];
                if(FI.feeling.Length <2)
                {
                    continue;
                }
                FI.type = tunteenTyyppi;
                FI.id = (i + 1) * 1000;
                listOfFeelings.Add(FI);
                howManyFeelings++;
                Debug.Log(FI.feeling);
            }

        }

    }
    */
    /**
     * 
     * @return lista, jossa on väittämälistoja
     */
    public List<FeelingInterface> getListOfFeelings()
    {
        return listOfFeelings;
    }

    public void addToListOfFeelings(FeelingInterface feel)
    {
        listOfFeelings.Add(feel);
        howManyFeelings++;
    }

    /**
     * Palauttaa ensimmäisen tason solmun id.tä vastaavan listan väittämistä.
     *
     * @param solmunID 0-5
     * @return lista väittämistä
     **/
    /*
   public ArrayList getAccusationsOfOneType(int ID)
   {
       if (ID == 0)
       {
           return (ArrayList)listOfFeelings[0];
       }
       return (ArrayList)listOfFeelings[ID];
   }

   public int getHowManyFeelings()
   {
       return howManyFeelings;
   }
   */


}