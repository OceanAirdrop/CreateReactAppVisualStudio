import React, { CSSProperties } from 'react';

import * as mobx from 'mobx'
import { observer, inject, observerBatching, useLocalStore } from 'mobx-react'

import styles from './SampleClassPage.module.scss';

// Import Stores
import { StoreDevTest } from '../../global-state/StoreDevTest'

import LocalState from './SampleClassPage.localstate';

type Props = {
  storeDev?    : StoreDevTest;         // injected mobx store
  storeAppList?: StoreAppList;         // injected mobx store
  title?       : string;               // passed as <DashboardPage title="my title">
};

@inject('storeDev')
@observer
class SampleClassPage extends React.Component<Props> {

  localState: LocalState = new LocalState()

  constructor(props: Props) {
    super(props);  
    this.state = this.localState;
    mobx.autorun(() => console.log("Local State Changed in", this.constructor.name, mobx.toJS(this.localState)));
  }

  componentDidMount() {
    console.log("SampleClassPage:componentDidMount");

    // As a test, lets update private state with the count from global state
    // get number of clicks from global store
    let clickCount = this.props.storeDev?.m_numClicks ?? 0;  
    this.localState.setCount(clickCount);
    
  }

  componentWillUnmount() {
    console.log("SampleClassPage:componentWillUnmount");
    // update globa store with local click count!
    if ( this.props.storeDev)
      this.props.storeDev.setClicks( this.localState.m_someNum );
  }

  render() {
    return (
      <div>
        <div className={styles.text}>hello {this.localState.m_someNum}</div>

        <button className={styles.button} onClick={this.buttonPress}>Click me</button>
      </div>)
  }

  buttonPress = () => {
    let newString =  Math.random().toString(36).substring(7) 
    this.localState.updateStringAndIncrementNum( newString );
  };

  changeSomeText = (newText: string) => {
    this.localState.changeSomeText( newText )
  };
}

export default SampleClassPage;