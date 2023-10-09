using System.ComponentModel;
using System.Globalization;

namespace Collections
{
    enum ItemID
    {   
        Arrow  = 10,
        RedPotion = 20,
        BluePotion = 21,
    }

    class SlotData : IComparable<SlotData>
    {
        public bool isEmpty => id == 0 && num == 0;

        public int id;
        public int num;

        public SlotData(int id, int num)
        {
            this.id = id;
            this.num = num;
        }

        public int CompareTo(SlotData? other)
        {
            return this.id == other?.id && this.num == other.num ? 0 : -1;  // 다른 것과 비교해서 같으면 정렬할수있게하는 기능으로 만들수있음.
        }                                                                    //아니면 비교해서같으면 해당개수를 합치게하는것도가능할것같음
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            MyDynamicArray inventory = new MyDynamicArray();

            for (int i = 0; i < 4; i++)
            {
                inventory.Add(new SlotData(0, 0));
            }
            inventory.Add(new SlotData(0, 0));
            inventory[3] = new SlotData((int)ItemID.BluePotion, 30);
           
            // todo -> 파란포션 5개를 획득
            // 1. 파란포션 5개가 들어갈 수 있는 슬롯을 찾아야함.   // obejct가 들어가고 bool값으로 나올텐데 match가 call되면 안의값을 비교하게됨 형식을 넘기고 기능은 나중에
            int availableSlotIndex = inventory.FindIndex(IsSlotData => ((SlotData)IsSlotData).isEmpty || // unboxing
                                                                       (((SlotData)IsSlotData).id == (int)ItemID.BluePotion &&
                                                                       ((SlotData)IsSlotData).num <= 99 - 20));
            object finditem = inventory.Find(FindItem => ((SlotData)FindItem).id == (int)ItemID.BluePotion);
                ItemID itemId = (ItemID)((SlotData)finditem).id;
            int itemNumber = (int)(ItemID)((SlotData)finditem).num;
            int itemIndex = (int)(ItemID)((SlotData)finditem).id;
                Console.WriteLine($"Found Item ID:" +
                    $"{itemId}, {itemIndex},{itemNumber}");
            
            /*  bool slotData(생략) (IsSlotData)    // 
             {
               return ((SlotData)IsSlotData).isEmpty ||  
                      (((SlotData)IsSlotData).id == (int)ItemID.BluePotion &&
                      ((SlotData)IsSlotData).num <= 99 - 20));  
                     slotData is Empty OR slotData.id == int BluePotion AND slotData.num <= 79 reutrn true or false;
              } */

            int availableSlotIndex2 = inventory.FindIndex(slotData2 => ((SlotData)slotData2).isEmpty ||
                                                                     (((SlotData)slotData2).id == (int)ItemID.RedPotion &&
                                                                      ((SlotData)slotData2).num <= 99 - 30));
                                              //FindIndex한테 value를 넘겨줄건데 만약 slotData라는 변수를 넣어줄테니까 안에 계산식들이 모두 통과되었을때 넘겨준다.

            // 2. 해당 슬롯의 아이템 갯수에다가 내가 추가하려는 갯수를 더한 만큼의 수정예상값을 구함.
            int expected = ((SlotData)inventory[availableSlotIndex]).num + 20;
            int expected2 = ((SlotData)inventory[availableSlotIndex2]).num + 30;

            // 3. 새로운 아이템 데이터를 만들어서 슬롯 데이터를 교체 해줌.
            SlotData newSlotData = new SlotData((int)ItemID.BluePotion, expected);
            SlotData newSlotData2 = new SlotData((int)ItemID.RedPotion, expected2);
            inventory[availableSlotIndex] = newSlotData;
            inventory[availableSlotIndex2] = newSlotData2;

            Console.WriteLine("인벤토리 정보 :");
           
            Console.WriteLine($"Capacity : {inventory.Capacity} , Counts : {inventory.Count}");

            object FindItemRemove = inventory.Find(slotdata => ((SlotData)slotdata).id == (int)ItemID.BluePotion); // 해당인덱스에있는 아이템이사라짐
            bool removed = inventory.Remove(FindItemRemove);
            Console.WriteLine(removed); 
           /* inventory.RemoveAt(3); // index 자체가 사라짐*/

            for (int i = 0; i < inventory.Count; i++)
            {
                Console.WriteLine($"슬롯 {i} : [{(ItemID)((SlotData)inventory[i]).id}] , [{((SlotData)inventory[i]).num}]");
            }
            MyDynamicArray<SlotData> inventory2 = new MyDynamicArray<SlotData>();

            // 숙제 : MyDynamicArray 기반으로 만들었던 예제를 inventory2 가지고 구현해보기

            /*
            inventory[1] = "철수";
            Console.WriteLine($"동적배열의 0번째 인덱스 값 : {inventory[0]}");

            int idx = inventory.FindIndex(x => (string)x == "철수");
            idx = inventory.FindIndex(x => (int)x > 3);

            int 파란포션5개추가가능한인덱스 = inventory.FindIndex(x => x.isEmpty == true ||
                                                                (x.id == 파란포션아이디 &&
                                                                 x.num <= 파란포션최대갯수 - 5))
            int 수정될갯수 = inventory[파란포션5개추가가능한인덱스].num + 5;
            슬롯데이터 수정될슬롯데이터 = new 슬롯데이터(파란포션아이디, 수정될갯수);
            inventory[파란포션5개추가가능한인덱스] = 수정될슬롯데이터;
            */
        }
    }
}