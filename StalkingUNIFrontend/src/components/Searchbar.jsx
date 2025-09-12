import { useEffect, useState } from 'react';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import IconButton from '@mui/material/IconButton';
import SearchIcon from '@mui/icons-material/Search';

import api from '../utilities/api'


export default function BasicTextFields({ updateSearchResult }) {
  const baseQuery = "getStudent?"

  const [formData, setFormData] = useState({
    nome: "",
    cognome: "",
    matricola: ""
  })

  const [toUpdate, setToUpdate] = useState(false)
  const [query, setQuery] = useState(baseQuery)

  const handleSubmit = async (e) => {
    e.preventDefault()


    /*costruzione della query*/

    let first = true
    //a volerlo fare senza tmp dovrei usare lo state anche per first, quindi evito
    let tmp = baseQuery

    for (let key in formData) {
      //se non e' stato messo il campo non aggiungerlo alla query
      if (!formData[key])
        continue

      if (!first)
        tmp += "&"
      else
        first = false

      tmp += `${key}=${formData[key]}`
    }

    setQuery(tmp)

    /*fine costruzione query*/

    //preparo variabile per la useEffect
    setToUpdate(true)
  }

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await api.get(`${query}`)
        //console.log("rispostaaaaaaa: ", response.data)
        updateSearchResult(response.data)
      } catch (error) {
        console.error('Error fetching user data:', error)
      }
    }

    //l'if e' necessario perche' poi rimetto ToUpdate a false, quindi la UseEffect viene chiamata nuovamente
    if (toUpdate) {
      fetchData()
      setToUpdate(false)
    }

  }, [toUpdate])

  const handleInputChange = (e) => {
    setFormData({
      ...formData,
      [e.target.name]: e.target.value
    })
  }

  return (
    <Box
      component="form"
      sx={{ '& > :not(style)': { m: 3 }, borderRadius: 4, border: 1 }}
      onSubmit={handleSubmit}
    >
      <TextField sx={{ width: '20ch' }} name="nome" label="Nome" variant="standard" value={formData.nome} onChange={handleInputChange} />
      <TextField sx={{ width: '20ch' }} name="cognome" label="Cognome" variant="standard" value={formData.cognome} onChange={handleInputChange} />
      <TextField sx={{ width: '8ch' }} name="matricola" label="Matricola" variant="standard" value={formData.matricola} onChange={handleInputChange} />
      <IconButton aria-label="search" type="submit" color="primary">
        <SearchIcon />
      </IconButton>
    </Box>
  );
}
