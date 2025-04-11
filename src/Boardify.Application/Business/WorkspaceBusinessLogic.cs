using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Features.Workspaces.Queries.Models;
using Boardify.Application.Interfaces.Business;
using Boardify.Application.Interfaces.Specific.Workspaces;
using FluentValidation;

namespace Boardify.Application.Business
{
    public class WorkspaceBusinessLogic : IWorkspaceBusinessLogic
    {
        private readonly IValidator<CreateWorkspaceModel> _createWorkspaceValidator;
        private readonly IValidator<UpdateWorkspaceModel> _updateWorkspaceValidator;
        private readonly IValidator<RemoveMemberFromWorkspaceModel> _removeMemberFromWorkspaceValidator;
        private readonly IValidator<AddMemberToWorkspaceModel> _addMemberToWorkspaceValidator;
        private readonly ICreateWorkspaceCommand _createWorkspaceCommand;
        private readonly IUpdateWorkspaceCommand<UpdateWorkspaceModel, UpdateWorkspaceResponseModel> _updateWorkspaceCommand;
        private readonly IAddMemberToWorkspaceCommand _addUserToWorkspaceCommand;
        private readonly IRemoveUserWorkspaceCommand _removeUserWorkspaceCommand;
        private readonly IGetWorkspaceMembersQuery _getWorkspaceMembersQuery;
        private readonly IGetOwnedWorkspaceQuery _getOwnedWorkspacesQuery;
        private readonly IResultFactory _resultFactory;

        public WorkspaceBusinessLogic(
                  IValidator<CreateWorkspaceModel> createWorkspaceValidator,
                  IValidator<UpdateWorkspaceModel> updateWorkspaceValidator,
                  ICreateWorkspaceCommand createWorkspaceCommand,
                  IUpdateWorkspaceCommand<UpdateWorkspaceModel, UpdateWorkspaceResponseModel> updateWorkspaceCommand,
                  IAddMemberToWorkspaceCommand addUserToWorkspaceCommand,
                  IRemoveUserWorkspaceCommand removeUserWorkspaceCommand,
                  IGetWorkspaceMembersQuery getWorkspaceMembersQuery,
                  IGetOwnedWorkspaceQuery getOwnedWorkspacesQuery,
                  IValidator<RemoveMemberFromWorkspaceModel> removeMemberFromWorkspaceValidator,
                  IValidator<AddMemberToWorkspaceModel> addMemberToWorkspaceValidator,
                  IResultFactory resultFactory)
        {
            _createWorkspaceValidator = createWorkspaceValidator ?? throw new ArgumentNullException(nameof(createWorkspaceValidator));
            _updateWorkspaceValidator = updateWorkspaceValidator ?? throw new ArgumentNullException(nameof(updateWorkspaceValidator));
            _createWorkspaceCommand = createWorkspaceCommand ?? throw new ArgumentNullException(nameof(createWorkspaceCommand));
            _updateWorkspaceCommand = updateWorkspaceCommand ?? throw new ArgumentNullException(nameof(updateWorkspaceCommand));
            _addUserToWorkspaceCommand = addUserToWorkspaceCommand ?? throw new ArgumentNullException(nameof(addUserToWorkspaceCommand));
            _removeUserWorkspaceCommand = removeUserWorkspaceCommand ?? throw new ArgumentNullException(nameof(removeUserWorkspaceCommand));
            _getWorkspaceMembersQuery = getWorkspaceMembersQuery ?? throw new ArgumentNullException(nameof(getWorkspaceMembersQuery));
            _getOwnedWorkspacesQuery = getOwnedWorkspacesQuery ?? throw new ArgumentNullException(nameof(getOwnedWorkspacesQuery));
            _removeMemberFromWorkspaceValidator = removeMemberFromWorkspaceValidator ?? throw new ArgumentNullException(nameof(removeMemberFromWorkspaceValidator));
            _addMemberToWorkspaceValidator = addMemberToWorkspaceValidator ?? throw new ArgumentNullException(nameof(addMemberToWorkspaceValidator));
            _resultFactory = resultFactory;
        }

        #region WorkspaceBl

        public async Task<IResult<IEnumerable<GetOwnedWorkspacesModel>>> GetOwnedWorkspaces(int userId)
        {
            return await _getOwnedWorkspacesQuery.Execute(userId);
        }

        public async Task<IResult<CreateWorkspaceResponseModel>> CreateWorkspace(CreateWorkspaceModel model, int creatorUserId)
        {
            var validation = await _createWorkspaceValidator.ValidateAsync(model);

            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );

                return _resultFactory.Failure<CreateWorkspaceResponseModel>(errorDictionary);
            }

            return await _createWorkspaceCommand.Create(model, creatorUserId);
        }

        public async Task<IResult<UpdateWorkspaceResponseModel>> UpdateWorkspace(int id, UpdateWorkspaceModel updateWorkspace, int creatorUserId)
        {
            var validation = await _updateWorkspaceValidator.ValidateAsync(updateWorkspace);

            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );

                return _resultFactory.Failure<UpdateWorkspaceResponseModel>(errorDictionary);
            }

            return await _updateWorkspaceCommand.Update(id, updateWorkspace, creatorUserId);
        }

        #endregion WorkspaceBl

        #region UserWorkspace

        public async Task<IResult<AddMemberToWorkspaceResponseModel>> AddMemberToWorkspace(AddMemberToWorkspaceModel model, int creatorUserId)
        {
            var validation = await _addMemberToWorkspaceValidator.ValidateAsync(model);
            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => string.Join("; ", g.Select(e => e.ErrorMessage))
                    );

                return _resultFactory.Failure<AddMemberToWorkspaceResponseModel>(errorDictionary);
            }

            var result = await _addUserToWorkspaceCommand.Create(model, creatorUserId);
            return result;
        }

        public async Task<IResult<bool>> RemoveMemberFromWorkspace(RemoveMemberFromWorkspaceModel model)
        {
            var result = await _removeUserWorkspaceCommand.Delete(model);

            if (!result.IsSuccess())
            {
                return _resultFactory.Failure<bool>(result.Message);
            }

            return _resultFactory.Success(true);
        }
        public async Task<IResult<IEnumerable<WorkspaceMemberModel>>> GetWorkspaceMembersAsync(int workspaceId)
        {
            return await _getWorkspaceMembersQuery.GetWorkspaceMembersAsync(workspaceId);
        }

        #endregion UserWorkspace
    }
}