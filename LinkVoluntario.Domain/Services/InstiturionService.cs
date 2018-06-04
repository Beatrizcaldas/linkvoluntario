﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinkVoluntario.Domain.Entities;
using LinkVoluntario.Domain.Interfaces;

namespace LinkVoluntario.Domain.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IInstitutionRepository _institutionRepository;

        public InstitutionService(IInstitutionRepository institutionRepository)
        {
            _institutionRepository = institutionRepository;
        }
        public bool Edit(Institution instituicao)
        {
            return _institutionRepository.Edit(instituicao);
        }

        public bool Delete(long InstituicaoId)
        {
            return _institutionRepository.Delete(InstituicaoId);
        }

        public bool Register(Institution instituicao)
        {
            return _institutionRepository.Register(instituicao);
        }

        public IList<Institution> ListAll()
        {
            return _institutionRepository.ListAll();
        }
    }
}
