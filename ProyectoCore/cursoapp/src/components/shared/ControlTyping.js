import React, {useState, useEffect} from 'react'

export default function ControlTyping(texto, delay){
    const [textValue, setTextValue] = useState();

    useEffect(() => {
        const handler = setTimeout(() => {
            setTextValue(texto);
        }, delay);

        return () => {
            clearTimeout(handler);
        }
    }, [texto]);

    return textValue;
}