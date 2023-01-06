Imports System
Imports System.Runtime.CompilerServices
Imports ManShop.Model.ManShop.Model.Models
Imports ManShop.Web.ManShop.Web.Models

Namespace ManShop.Web.Infrastructure.Extensions
    Public Module EntityExtensions
        <Extension()>
        Public Sub UpdatePostCategory(ByVal postCategory As PostCategory, ByVal postCategoryVm As PostCategoryViewModel)
            postCategory.ID = postCategoryVm.ID
            postCategory.Name = postCategoryVm.Name
            postCategory.Description = postCategoryVm.Description
            postCategory.Alias = postCategoryVm.Alias
            postCategory.ParentID = postCategoryVm.ParentID
            postCategory.DisplayOrder = postCategoryVm.DisplayOrder
            postCategory.Image = postCategoryVm.Image
            postCategory.HomeFlag = postCategoryVm.HomeFlag

            postCategory.CreatedDate = postCategoryVm.CreatedDate
            postCategory.CreatedBy = postCategoryVm.CreatedBy
            postCategory.UpdatedDate = postCategoryVm.UpdatedDate
            postCategory.UpdatedBy = postCategoryVm.UpdatedBy
            postCategory.MetaKeyword = postCategoryVm.MetaKeyword
            postCategory.MetaDescription = postCategoryVm.MetaDescription
            postCategory.Status = postCategoryVm.Status

        End Sub
        <Extension()>
        Public Sub UpdateProductCategory(ByVal productCategory As ProductCategory, ByVal productCategoryVm As ProductCategoryViewModel)
            productCategory.ID = productCategoryVm.ID
            productCategory.Name = productCategoryVm.Name
            productCategory.Description = productCategoryVm.Description
            productCategory.Alias = productCategoryVm.Alias
            productCategory.ParentID = productCategoryVm.ParentID
            productCategory.DisplayOrder = productCategoryVm.DisplayOrder
            productCategory.Image = productCategoryVm.Image
            productCategory.HomeFlag = productCategoryVm.HomeFlag

            productCategory.CreatedDate = productCategoryVm.CreatedDate
            productCategory.CreatedBy = productCategoryVm.CreatedBy
            productCategory.UpdatedDate = productCategoryVm.UpdatedDate
            productCategory.UpdatedBy = productCategoryVm.UpdatedBy
            productCategory.MetaKeyword = productCategoryVm.MetaKeyword
            productCategory.MetaDescription = productCategoryVm.MetaDescription
            productCategory.Status = productCategoryVm.Status

        End Sub
        <Extension()>
        Public Sub UpdatePost(ByVal post As Post, ByVal postVm As PostViewModel)
            post.ID = postVm.ID
            post.Name = postVm.Name
            post.Description = postVm.Description
            post.Alias = postVm.Alias
            post.CategoryID = postVm.CategoryID
            post.Content = postVm.Content
            post.Image = postVm.Image
            post.HomeFlag = postVm.HomeFlag
            post.ViewCount = postVm.ViewCount

            post.CreatedDate = postVm.CreatedDate
            post.CreatedBy = postVm.CreatedBy
            post.UpdatedDate = postVm.UpdatedDate
            post.UpdatedBy = postVm.UpdatedBy
            post.MetaKeyword = postVm.MetaKeyword
            post.MetaDescription = postVm.MetaDescription
            post.Status = postVm.Status
        End Sub

        <Extension()>
        Public Sub UpdateProduct(ByVal product As Product, ByVal productVm As ProductViewModel)
            product.ID = productVm.ID
            product.Name = productVm.Name
            product.Description = productVm.Description
            product.Alias = productVm.Alias
            product.CategoryID = productVm.CategoryID
            product.Content = productVm.Content
            product.Image = productVm.Image
            product.MoreImages = productVm.MoreImages
            product.Price = productVm.Price
            product.PromotionPrice = productVm.PromotionPrice
            product.Warranty = productVm.Warranty
            product.HomeFlag = productVm.HomeFlag
            product.HotFlag = productVm.HotFlag
            product.ViewCount = productVm.ViewCount

            product.CreatedDate = productVm.CreatedDate
            product.CreatedBy = productVm.CreatedBy
            product.UpdatedDate = productVm.UpdatedDate
            product.UpdatedBy = productVm.UpdatedBy
            product.MetaKeyword = productVm.MetaKeyword
            product.MetaDescription = productVm.MetaDescription
            product.Status = productVm.Status
            product.Tags = productVm.Tags
            product.Quantity = productVm.Quantity
            product.OriginalPrice = productVm.OriginalPrice
        End Sub

        <Extension()>
        Public Sub UpdateFeedback(ByVal feedback As Feedback, ByVal feedbackVm As FeedbackViewModel)
            feedback.Name = feedbackVm.Name
            feedback.Email = feedbackVm.Email
            feedback.Message = feedbackVm.Message
            feedback.Status = feedbackVm.Status
            feedback.CreatedDate = Date.Now
        End Sub

        <Extension()>
        Public Sub UpdateOrder(ByVal order As Order, ByVal orderVm As OrderViewModel)
            order.CustomerName = orderVm.CustomerName
            order.CustomerAddress = orderVm.CustomerName
            order.CustomerEmail = orderVm.CustomerName
            order.CustomerMobile = orderVm.CustomerName
            order.CustomerMessage = orderVm.CustomerName
            order.PaymentMethod = orderVm.CustomerName
            order.CreatedDate = Date.Now
            order.CreatedBy = orderVm.CreatedBy
            order.Status = orderVm.Status
            order.CustomerId = orderVm.CustomerId
        End Sub

        <Extension()>
        Public Sub UpdateApplicationGroup(ByVal appGroup As ApplicationGroup, ByVal appGroupViewModel As ApplicationGroupViewModel)
            appGroup.ID = appGroupViewModel.ID
            appGroup.Name = appGroupViewModel.Name
        End Sub

        <Extension()>
        Public Sub UpdateApplicationRole(ByVal appRole As ApplicationRole, ByVal appRoleViewModel As ApplicationRoleViewModel, ByVal Optional action As String = "add")
            If Equals(action, "update") Then
                appRole.Id = appRoleViewModel.Id
            Else
                appRole.Id = Guid.NewGuid().ToString()
            End If
            appRole.Name = appRoleViewModel.Name
            appRole.Description = appRoleViewModel.Description
        End Sub
        <Extension()>
        Public Sub UpdateUser(ByVal appUser As ApplicationUser, ByVal appUserViewModel As ApplicationUserViewModel, ByVal Optional action As String = "add")

            appUser.Id = appUserViewModel.Id
            appUser.FullName = appUserViewModel.FullName
            appUser.BirthDay = appUserViewModel.BirthDay
            appUser.Email = appUserViewModel.Email
            appUser.UserName = appUserViewModel.UserName
            appUser.PhoneNumber = appUserViewModel.PhoneNumber
        End Sub
    End Module
End Namespace
