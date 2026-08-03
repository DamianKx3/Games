using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class titded : MonoBehaviour
{
    public Text Text;
    void Start()
    {
        switch(PlayerPrefs.GetInt("JID"))
        {
            case 0:
                Text.text = "odleżynow, niżarłak, nibęben, śmierdziuch, von bębenov, Prezydent, knur, męczennik boży, mleczny król, kandydat na kandydata, Potężny Warmianin, konon, kśiek, fekał pryncypał, mlekołak: \n patrz na niego przez kamery wtedy się nie rusza, gdy na niego nie patrzysz pomimo obżarstwa potrafi szybko się przemieszczać";
                break;
            case 1:
                Text.text = "nitrowojtek, bombas, wojtek z bombasu, nitroszczur, nitrokukła, jaj00r, żądło boże, narkoman, kałjor, Ptasibrzuch, Galipados, Nitromumia, nitrodolski: \n w swoim nitropokoiku ma dostęp do wentylacji prowadzącej do twojej posiadówy, pociągnij wajche aby wpuscić do wentylacji opary rozpuszczalnika i zatrzymać nitokukłę";
                break;
            case 2:
                Text.text = "meksyk, mexicano, meksikikano, urynator, urynowicz, słonowaty, degustator, jareczek, redaktor naczelny, Pedro, Moczopijano, Meksykanin: \n gdy pojawi się przy lewych drzwiach, zamknij je i odczekaj aż pojawi sie spowrotem w swoim pokoju";
                    break;
            case 3:
                Text.text = "janek , ełborodo, wujek janek, Rodokop, Łoszyngoł, Juan deere, Jan Debesta: \n  gdy pojawi się przy prawych drzwiach, zamknij je i odczekaj aż pojawi sie spowrotem w swoim pokoju";
                    break;
            case 4:
                Text.text = "G?o-ld;e'n K*o.n/o,n: \n jest to złota wersja białoruskiego agenta, gdy zobaczysz w monitorze taniec burzy natychmiast rozglądaj sie po pokoju, uwaga! złoty mlekołak zmienia pozycje, szukaj go dopuki taniec nie ustanie";
                break;
            case 5:
                Text.text = "nitro kukła: proste, nakręcaj ją przytrzymując LPM patrząc na pudło, jak wyjdzie z pudła to smierć";
                break;

        }
    }

    
    void Update()
    {
        
    }
}
