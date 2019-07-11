﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using TrackerLibrary.DataAccess.TextHelpers;

namespace TrackerLibrary.DataAccess
{
    public class TextConnector : IDataConnection
    {

        private const string PrizesFile = "PrizeModels.csv"; //pascale case because const
        private const string PeopleFile = "PersonModels.csv";

        public PersonModel CreatePerson(PersonModel model)
        {
            List<PersonModel> people = PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();

            int currentId = 1;

            if(people.Count >0)
            {
                currentId = people.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId;
            people.Add(model);

            people.SaveToPeopleFile(PeopleFile);

            return model;
        }


        /// <summary>
        /// saves a new prize to the txt
        /// </summary>
        /// <param name="model">prize info</param>
        /// <returns>The prize info, including the unique identifier</returns>
        public PrizeModel CreatePrize(PrizeModel model)
        {
            // loads and converts to list 
            List<PrizeModel> prizes = PrizesFile.FullFilePath().LoadFile().ConvertToPrizeModels();

            //finds highest id in a list, gets +1 for the new record
            //checks if the first value is 1
            int currentId = 1;
            
            if (prizes.Count >0 ) //file is not empty, gets highest 
            {
                currentId = currentId = prizes.OrderByDescending(x => x.Id).First().Id + 1;
            }

            model.Id = currentId; //empty file, id becomes 1

            //adds record with a new id
            prizes.Add(model);

            //makes a list and saves
            prizes.SaveToPrizeFile(PrizesFile);

            //fully formed model with id that can be used further
            return model;

        }

        public List<PersonModel> GetPerson_All()
        {
            return PeopleFile.FullFilePath().LoadFile().ConvertToPersonModels();
        }
    }
}
