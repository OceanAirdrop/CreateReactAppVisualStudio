import React, { useState, useEffect } from 'react';
import { inject, observer } from 'mobx-react'

// Import Stores
import { StoreDevTest } from '../../global-state/StoreDevTest'
import { StoreAppList } from '../../global-state/StoreAppList'
import { StoreAuth } from '../../global-state/StoreAuth'

// Define Props Type
type SomePageProps = {
    storeDev?     : StoreDevTest;    // injected mobx store
    storeAppList? : StoreAppList;    // injected mobx store
    title?        : string;          // passed as <SomePage title="my title">
};

function SomePage(props: SomePageProps) {

    const [count, setCount] = useState(0);

    function clickHandler() {
        setCount(count + 1);
        props.storeDev?.incrementCount();
    }

    return (
        <div>
            <h1>Some Page</h1>
            <p>You clicked {count} times</p>
            <p>RootStore NumClick: {props.storeDev?.m_numClicks} times</p>
            <button onClick={clickHandler}>Click me</button>
        </div>
    );
}
export default inject('storeDev', 'storeAuth')(observer(SomePage))


