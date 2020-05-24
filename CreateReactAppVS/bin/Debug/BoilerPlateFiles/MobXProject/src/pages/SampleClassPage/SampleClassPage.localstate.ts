import { observable } from 'mobx';
import * as mobx from 'mobx'

// Define the local state for the component and all the actions you can perform on the state
class LocalState {
  @observable m_someText: string = 'Play';
  @observable m_someNum: number = 0;
  
  constructor() {
    console.log('app local state constructor')
  }

  @mobx.action setCount = (cnt: number) => {
    this.m_someNum = cnt;
  };

  @mobx.action incrementNum = () => {
    this.m_someNum++;
  };

  @mobx.action updateStringAndIncrementNum = (newText: string) => {
    this.incrementNum();
    this.m_someText = newText;
  };

  @mobx.action changeSomeText = (newText: string) => {
    this.m_someText = newText
  };
}

export default LocalState;