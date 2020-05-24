import { observable, action } from 'mobx'

export class StoreDevTest {
  @observable 
  m_numClicks: number = 0
  
  @observable 
  m_someString: number = 0

  @observable 
  myList: Array<string> = new Array();

  @action setClicks = (cnt: number) => {
    this.m_numClicks = cnt;
  }

  @action incrementCount = () => {
    this.m_numClicks++;
  }
}

// Create the one and only store here.
const store = new StoreDevTest();

export default store

