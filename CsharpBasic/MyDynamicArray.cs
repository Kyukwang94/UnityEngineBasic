// PascalCase : 사용자정의 자료형이름, 함수 이름, 프로퍼티 이름, public/protected 멤버 변수 이름
// camelCase : 지역변수, (Unity API 활용할때는 public/protected 멤버 변수 이름도 이렇게 주로 씀)
// _camelCase : private 멤버 변수 이름
// snake_case : 상수 정의시
// UPPER_SNAKE_CASE : 상수 정의시
// m_Hungarian , iNum, fX <- 요즘날에는 잘안씀 : static 멤버변수 s_Instance

namespace Collections
{
    internal class MyDynamicArray
    {
       
        private const int DEFAULT_SIZE = 1;
       
        private object[] _items = new object[DEFAULT_SIZE]; // items[0] 부터 시작.
        public object this[int index] //MyDynamicArray inventory = new MyDynamicArray(); inventory = this why ? MyDynamicArray value
        {                           // inventory[0] format 지킬때만 불러준다.
            get //  var get = inventory[0]
            {
                if (index < 0 || index >= _count) //해당 방번호가 0보다 작은곳이거나 해당 방번호가 실질거주민의 주소보다 크거나 같으면.
                {
                    throw new IndexOutOfRangeException(); 
                }

                return _items[index]; // _items[index]의 주소로 접근해 안에 값을 주겠다.
            }
            set // inventroy[0] = new slotData((int)redPotion , 60)
            {
                if (index < 0 || index >= _count)
                {
                    throw new IndexOutOfRangeException();
                }

                _items[index] = value; //value는 main에서 선언된값이들어가는거
            }
        }
        private int _count;
        public int Count //_count 접근하고싶으면 Count를 통해서 불러라.
        {
            get
            {
                return _count;
            }
        }
        public int Capacity
        {
          get
            {
                return _items.Length;
            }
        }

       /* public int Count => _count;
        public int Capacity => _items.Length;*/

       
       

        public void Add(object item) // 현재꽉차있는 가방에 Add함수를 다시한번더 쓰면 가방칸을 늘려주거나 item을 추가해줌.
        {

           
            if (_count >= _items.Length)
            {
                object[] tmp = new object[_count * 2]; 
                Array.Copy(_items, tmp, _count); //기존배열 새로운배열에다가 어디까지 _count까지.
                _items = tmp; //tmp만큼 * 2 된 배열만큼 늘어남 하지만 값은 없음.
            }

            _items[_count++] = item;//새로운 아이템을 넣는다

        }

        public object Find(Predicate<object> match) 
        {
            for (int i = 0; i < _count; i++)
            {
                if (match(_items[i]))
                    return _items[i];
            }
            return default;
        }
       
        public int FindIndex(Predicate<object> match)  // 그저형식만체크
        {                                             
            for (int i = 0; i < _count; i++)         //count만큼 i를 올림.
            {
                if (match(_items[i]))  //items[i]를 lambda변수 SlotData에 넘겨줌. match는 해당 파라미터로 인해 lambda조건체크.
                    return i;          //해당 i에서 match되면 return  i (지금 index)
            }
            return -1;
        }

        public bool Contains(object item)//해당 아이템이잇는지 확인
        {
            for (int i = 0; i < _count; i++)
            {
                if (_items[i] == item)
                    return true;
            }

            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= _count) //해당인덱스가 범위인덱스에서 벗어났을때
                throw new IndexOutOfRangeException();

            for (int i = index; i < _count - 1; i++)
            {
                _items[i] = _items[i + 1];
            }
            _count--;
        }

        public bool Remove(object item)
        {
            int index = FindIndex(x => x == item);

            if (index < 0)
                return false;

            RemoveAt(index);
            return true;
        }
    }
}
