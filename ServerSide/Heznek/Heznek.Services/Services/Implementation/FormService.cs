using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Heznek.Common.Enums;
using Heznek.DataAccess.Entities;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Helpers;
using Heznek.Services.Models;
using Heznek.Services.Providers;

namespace Heznek.Services.Implementation
{
    public class FormService : AbstractService, IFormService
    {
        private readonly IFileHelper _fileHelper;
        public FormService(IUnitOfWork unitOfWork, 
            IFileHelper fileHelper,
            IAuthenticatedUser authUser)
            : base(unitOfWork, authUser)
        {
            _fileHelper = fileHelper;
        }

        public FormModel GetForm(string userId)
        {
            var form = _unitOfWork.Repository<FormEntity>().Set
                .Where(x => x.UserId == userId)
                .Select(x => new FormModel
                {
                    Status = x.Status,
                    ParentsSalary = new FormParentsSalaryModel
                    {
                        Id = x.ParentsSalary.Id,
                        Name = x.ParentsSalary.Name,
                        LastUpdated = x.ParentsSalary.LastUpdated,
                        FatherName = x.ParentsSalary.FatherName,
                        MotherName = x.ParentsSalary.MotherName,
                        FatherDisability = x.ParentsSalary.FatherDisability,
                        MotherDisability = x.ParentsSalary.MotherDisability,
                        SalarySlipsFileName = x.ParentsSalary.SalarySlips,
                        DisabilityFileName = x.ParentsSalary.Disability,
                        DisabilityDownloadName = x.ParentsSalary.DisabilityDownloadName,
                        Disability2FileName = x.ParentsSalary.Disability2,
                        Disability2DownloadName = x.ParentsSalary.Disability2DownloadName,
                        SalarySlipsDownloadName = x.ParentsSalary.SalarySlipsDownloadName,
                        Disability = null,
                        Disability2 = null,
                        SalarySlips = null,
                    },
                    Tasks = x.Tasks.Select(n => new FormTaskModel
                    {
                        Id = n.Id,
                        Name = n.Name,
                        FileName = n.FileName,
                        LastUpdated = n.LastUpdated,
                        DownloadName = n.DownloadName,
                        File = null
                    }).ToList()
                })
                .FirstOrDefault();

            if(form == null){
                var x = GenerateForm(userId);
                _unitOfWork.Repository<FormEntity>().Insert(x);
                form = new FormModel
                {
                    Status = x.Status,
                    ParentsSalary = new FormParentsSalaryModel
                    {
                        Id = x.ParentsSalary.Id,
                        Name = x.ParentsSalary.Name,
                        LastUpdated = x.ParentsSalary.LastUpdated,
                        FatherName = x.ParentsSalary.FatherName,
                        MotherName = x.ParentsSalary.MotherName,
                        FatherDisability = x.ParentsSalary.FatherDisability,
                        MotherDisability = x.ParentsSalary.MotherDisability,
                        SalarySlipsFileName = x.ParentsSalary.SalarySlips,
                        DisabilityFileName = x.ParentsSalary.Disability,
                        Disability2FileName = x.ParentsSalary.Disability2,
                        SalarySlipsDownloadName = string.Empty,
                        DisabilityDownloadName = string.Empty,
                        Disability = null,
                        Disability2DownloadName = string.Empty,
                        Disability2 = null,
                        SalarySlips = null,
                    },
                    Tasks = x.Tasks.Select(n => new FormTaskModel
                    {
                        Id = n.Id,
                        Name = n.Name,
                        FileName = n.FileName,
                        LastUpdated = n.LastUpdated,
                        DownloadName = string.Empty,
                        File = null
                    }).ToList()
                };
            }
            else
            {
                form.ParentsSalary.DisabilityDownloadName = string.IsNullOrEmpty(form.ParentsSalary.DisabilityFileName) ? "" :
                                                    "Users/" + userId + "/" + form.ParentsSalary.DisabilityDownloadName + "." +
                                                    form.ParentsSalary.DisabilityFileName.Split('.').LastOrDefault();
                form.ParentsSalary.Disability2DownloadName = string.IsNullOrEmpty(form.ParentsSalary.Disability2FileName) ? "" :
                                                    "Users/" + userId + "/" + form.ParentsSalary.Disability2DownloadName + "." +
                                                    form.ParentsSalary.Disability2FileName.Split('.').LastOrDefault();
                form.ParentsSalary.SalarySlipsDownloadName = string.IsNullOrEmpty(form.ParentsSalary.SalarySlipsFileName) ? "" :
                                                    "Users/" + userId + "/" + form.ParentsSalary.SalarySlipsDownloadName + "." +
                                                    form.ParentsSalary.SalarySlipsFileName.Split('.').LastOrDefault();
                form.Tasks.ForEach(x => 
                    x.DownloadName = string.IsNullOrEmpty(x.FileName) ? "" :
                                        "Users/" + userId + "/" + x.DownloadName + "." +
                                        x.FileName.Split('.').LastOrDefault()
                );

            }
            return form;
        }

        public FormEntity GenerateForm(string userId)
        {
            return new FormEntity
            {
                UserId = userId,
                Status = FormStatusEnum.InProgress,
                ParentsSalary = new FormParentsSalaryEntity
                {
                    Name = "תלושי שכר חודשי של ההורים",
                    SalarySlipsDownloadName = "SalarySlips",
                    DisabilityDownloadName = "Disability",
                    Disability2DownloadName = "Disability2",
                },
                Tasks = new List<FormTaskEntity>
                    {
                        new FormTaskEntity{ Name = @"תצלום תעודת זהות", DownloadName = "IDCard" },
                        new FormTaskEntity{ Name = @"תעודת שחרור משירות צבאי/לאומי", DownloadName = "MSReleaseCert" },
                        new FormTaskEntity{ Name = @"תעודת הערכה משירות צבאי/לאומי", DownloadName = "AppreciationCert" },
                        new FormTaskEntity{ Name = @"צילום תעודת בגרות/סיום" , DownloadName = "Diploma"},
                        new FormTaskEntity{ Name = @"המלצות(במידה ויש)", DownloadName = "Recomendations" },
                    }
            };
        }

        public FormModel GetForm()
        {
            return GetForm(_authUser.Id);
        }

        public async Task SendForm()
        {
            await ChangeFormStatus(_authUser.Id, FormStatusEnum.Reviewing);
        }

        public async Task<FormParentsSalaryModel> UpdateParentsSalary(FormParentsSalaryModel model)
        {
            var entity = _unitOfWork.Repository<FormParentsSalaryEntity>().Set.FirstOrDefault(x => x.Form.UserId == _authUser.Id);
            if(entity != null)
            {
                entity.MotherName = model.MotherName;
                entity.FatherName = model.FatherName;
                entity.FatherDisability = model.FatherDisability;
                entity.MotherDisability = model.MotherDisability;
                entity.LastUpdated = DateTime.Now;
                model.LastUpdated = entity.LastUpdated;

                if (!string.IsNullOrEmpty(model.SalarySlips?.FileName))
                {

                    //var name = $"Form_ParentsSalary_{nameof(entity.SalarySlips)}";
                    var oldName = string.IsNullOrEmpty(entity.SalarySlips) ? "" : entity.SalarySlipsDownloadName + entity.SalarySlips.Split('.').LastOrDefault();
                    var newName = await _fileHelper.SaveOrUpdateUserFile(model.SalarySlips, oldName, entity.SalarySlipsDownloadName, _authUser.Id);
                    
                    model.SalarySlipsDownloadName = newName;
                    //entity.SalarySlipsDownloadName = newName;
                    model.SalarySlipsFileName = model.SalarySlips.FileName;
                    entity.SalarySlips = model.SalarySlips.FileName;

                    model.SalarySlips = null;
                }

                if (!string.IsNullOrEmpty(model.Disability?.FileName))
                {
                    //var name = $"Form_ParentsSalary_{nameof(entity.Disability)}";
                    var oldName = string.IsNullOrEmpty(entity.Disability) ? "" : entity.DisabilityDownloadName + entity.Disability.Split('.').LastOrDefault();
                    var newName = await _fileHelper.SaveOrUpdateUserFile(model.Disability, oldName, entity.DisabilityDownloadName, _authUser.Id);

                    model.DisabilityDownloadName = newName;
                    //entity.SalarySlipsDownloadName = newName;
                    model.DisabilityFileName = model.Disability.FileName;
                    entity.Disability = model.Disability.FileName;

                    model.Disability = null;
                }

                if (!string.IsNullOrEmpty(model.Disability2?.FileName))
                {
                    //var name = $"Form_ParentsSalary_{nameof(entity.Disability)}";
                    var oldName = string.IsNullOrEmpty(entity.Disability2) ? "" : entity.Disability2DownloadName + entity.Disability2.Split('.').LastOrDefault();
                    var newName = await _fileHelper.SaveOrUpdateUserFile(model.Disability2, oldName, entity.Disability2DownloadName, _authUser.Id);

                    model.Disability2DownloadName = newName;
                    //entity.SalarySlipsDownloadName = newName;
                    model.Disability2FileName = model.Disability2.FileName;
                    entity.Disability2 = model.Disability2.FileName;

                    model.Disability2 = null;
                }

                await _unitOfWork.Repository<FormParentsSalaryEntity>().UpdateAsync(entity);
                return model;
            }

            return null;
        }

        public async Task<FormTaskModel> UpdateTask(FormTaskModel model)
        {
            var entity = _unitOfWork.Repository<FormTaskEntity>().Set.FirstOrDefault(x => x.Id == model.Id && x.Form.UserId == _authUser.Id);
            if(entity != null)
            {
                entity.LastUpdated = DateTime.Now;
                model.LastUpdated = entity.LastUpdated;
                //var name = Regex.Replace($"Form_{entity.Name}", "[/,.\\']", "_");
                var oldName = string.IsNullOrEmpty(entity.FileName) ? "" : entity.DownloadName + entity.FileName.Split('.').LastOrDefault();

                var newName = await _fileHelper.SaveOrUpdateUserFile(model.File,oldName, entity.DownloadName, _authUser.Id);
                model.DownloadName = newName;
                //entity.DownloadName = newName;
                model.FileName = model.File.FileName;
                entity.FileName = model.File.FileName;
                model.File = null;

                await _unitOfWork.Repository<FormTaskEntity>().UpdateAsync(entity);
                return model;

            }
            return null;
        }

        public async Task ChangeFormStatus(string UserId, FormStatusEnum status)
        {
            var entity = _unitOfWork.Repository<FormEntity>().Set.FirstOrDefault(x => x.UserId == UserId);
            if (entity != null)
            {
                entity.Status = status;
                await _unitOfWork.Repository<FormEntity>().UpdateAsync(entity);
            }
        }
    }
}
