using System.Collections.Generic;


public class GameHelperFunction
{
   public static T RandomSelect<T>(List<T> beSelectedList, int min = 0 , int max = -1){
        if(beSelectedList == null || beSelectedList.Count == 0){
            return default (T);
        }
        System.Random random = new System.Random();
        int index = 0;
        if(min > max){
            index = random.Next(min,beSelectedList.Count);
        }else{
            index = random.Next(min,max);
        }
        
        return beSelectedList[index];
        
    }

}
